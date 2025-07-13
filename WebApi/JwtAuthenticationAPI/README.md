# JWT Authentication API

This Web API project demonstrates JSON Web Token (JWT) authentication in ASP.NET Core.

## Features

1. JWT Token Generation
2. Role-based Authorization
3. Token Expiration (2 minutes)
4. Employee CRUD Operations

## How to Test the API using Postman

### 1. Generate a JWT Token

Make a GET request to:
```
https://localhost:5001/api/Auth
```

This will return a JWT token that is valid for 2 minutes with "Admin" role.

You can also specify a user ID and role:
```
https://localhost:5001/api/Auth?userId=123&role=Admin
```

### 2. Access Protected Endpoints

Use the token in the Authorization header for requests to Employee endpoints:

1. Get all employees:
   - Method: GET
   - URL: `https://localhost:5001/api/Employee`
   - Headers: 
     - Key: `Authorization`
     - Value: `Bearer {your_token_here}`

2. If the token is valid and contains the correct role ("Admin" or "POC"), you should receive a 200 OK response.
3. If the token is invalid or expired, you'll receive a 401 Unauthorized response.
4. If the token is valid but doesn't have the required role, you'll receive a 403 Forbidden response.

### Testing Scenarios

1. **Valid Token**: Use the generated token immediately to access Employee endpoints
2. **Expired Token**: Wait for 2 minutes after generating the token, then try to access Employee endpoints
3. **Invalid Token**: Modify the token and try to access Employee endpoints
4. **Role Check**: Generate a token with a role other than "Admin" or "POC" and try to access Employee endpoints

## Implementation Details

- The `AuthController` generates JWT tokens with claims for user ID and role
- The `EmployeeController` requires authentication and authorizes only "Admin" and "POC" roles
- The token expires after 2 minutes as specified in `GenerateJSONWebToken` method
- The token validation parameters are configured in `Startup.cs`
