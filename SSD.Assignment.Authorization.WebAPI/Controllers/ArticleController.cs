using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSD.Assignment.Authorization.WebAPI.Controllers.Dtos;
using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : ControllerBase
    {
        private readonly IService<Article> _service;

        public ArticleController(IService<Article> service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles="Editor, Writer")]
        public IActionResult Get(int id)
        {
            try
            {
                var article = _service.Get(id);
                
                if (article == null)
                {
                    return NotFound();
                }
                
                return Ok(article);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] CreateArticleDto articleDto)
        {
            try
            {
                if (string.IsNullOrEmpty(articleDto.Title) || string.IsNullOrEmpty(articleDto.Content))
                {
                    return BadRequest("Title and/or content are required.");
                }
                
                var articleModel = new Article
                {
                    Title = articleDto.Title,
                    Content = articleDto.Content,
                    UserId = articleDto.UserId
                };

                var article = _service.Add(articleModel);

                return Ok("created article: \n" + article.Title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles="Editor, Writer")]
        public IActionResult Update(int id, UpdateArticleDto articleDto)
        {
            try
            {
                if (string.IsNullOrEmpty(articleDto.Title) || string.IsNullOrEmpty(articleDto.Content))
                {
                    return BadRequest("Title and/or content are required.");
                }
                
                var token = Request.Headers["Authorization"];
            
                var decodedToken = DecodeToken(token);
                
                if (decodedToken == null) return Unauthorized();
                
                // if the user is not an editor, they can only update their own articles
                if (!HasAccessToArticle(id, decodedToken)) return Unauthorized();
                
                var articleModel = new Article
                {
                    Id = id,
                    Title = articleDto.Title,
                    Content = articleDto.Content
                };

                var article = _service.Update(articleModel);

                return Ok("updated article: \n" + article.Title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpDelete("{articleId}")]
        [Authorize(Roles="Editor, Writer")]
        public IActionResult Delete(int articleId)
        {
            try
            {
                var token = Request.Headers["Authorization"];
            
                var decodedToken = DecodeToken(token);
                
                if (decodedToken == null) return Unauthorized();

                // if the user is not an editor, they can only delete their own articles
                if (!HasAccessToArticle(articleId, decodedToken)) return Unauthorized();
                
                var article = _service.Delete(articleId);

                return Ok("deleted article: " + article.Title);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static JwtSecurityToken? DecodeToken(StringValues token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            
            // very hacky I know...
            // remove the "Bearer" prefix
            token = token.ToString().Substring(7);

            return new JwtSecurityToken(token);
        }

        private bool HasAccessToArticle(int articleId, JwtSecurityToken decodedToken)
        {
            if (decodedToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value == "Editor") return true;
            {
                var article = _service.Get(articleId);

                var userid = decodedToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
                    
                if (userid != null && article.UserId != int.Parse(userid))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
