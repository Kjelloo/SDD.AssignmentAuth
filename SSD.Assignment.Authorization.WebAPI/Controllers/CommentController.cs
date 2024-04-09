using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSD.Assignment.Authorization.WebAPI.Controllers.Dtos;
using SSD.Assignment.Authorization.WebAPI.Model;
using SSD.Assignment.Authorization.WebAPI.Services.Interfaces;

namespace SSD.Assignment.Authorization.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_commentService.GetAll());
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        try
        {
            return Ok(_commentService.Get(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpGet("Article/{articleId}")]
    public IActionResult GetCommentsByArticleId(int articleId)
    {
        try
        {
            return Ok(_commentService.GetCommentsByArticleId(articleId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] CreateCommentDto commentDto)
    {
        try
        {
            var comment = new Comment
            {
                ArticleId = commentDto.ArticleId,
                UserId = commentDto.UserId,
                Content = commentDto.Content
            };
                
            return Ok(_commentService.Add(comment));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize]
    [HttpPut]
    public IActionResult Update([FromBody] Comment comment)
    {
        try
        {
            var decodedToken = DecodeToken(Request.Headers["Authorization"]);
            
            if (decodedToken == null)
                return BadRequest("Invalid token");

            if (!HasAccessToChangeComment(comment.Id, decodedToken)) return Unauthorized();
            
            return Ok(_commentService.Update(comment));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize(Roles = "Editor,User")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var decodedToken = DecodeToken(Request.Headers["Authorization"]);
            
            if (decodedToken == null)
                return BadRequest("Invalid token");

            if (!HasAccessToChangeComment(id, decodedToken)) return Unauthorized();
            
            return Ok(_commentService.Delete(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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

    private bool HasAccessToChangeComment(int commentId, JwtSecurityToken decodedToken)
    {
        if (decodedToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value == "Editor") 
            return true;
        
        var userid = decodedToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
        
        if (string.IsNullOrEmpty(userid))
            return false;
        
        var comments = _commentService.GetCommentsByUserId(int.Parse(userid)).ToList();
        
        return comments.Any(c => c.Id == commentId);
    }
}