# SDD.AssignmentAuth

Run the application with the following command: `dotnet run --urls=http://localhost:5266/` in the WebAPI directory.

Once the application is running, you can access the Swagger UI by navigating to `http://localhost:5266/swagger/index.html`.

The database is seeded with four users:
- Username: `editor`, Password: `editor` - with the Role: `Editor`
- Username: `writer`, Password: `writer` - with the Role: `Writer`
- Username: `writer2`, Password: `writer2` - with the Role: `Writer`
- Username: `user`, Password: `user` - with the Role: `User`

The application uses json web tokens for authorization. Once logging in through the `/api/User/Login` endpoint, you will receive a token that you can use to authenticate 
yourself in the Swagger UI by clicking the `Authorize` button and pasting the token. 

![auth.png](img%2Fauth.png)