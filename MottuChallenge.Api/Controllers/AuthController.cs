using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.Employees;
using MottuChallenge.Infrastructure.Security;

namespace MottuChallenge.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(CreateEmployeeUseCase _createEmployeeUseCase, LoginUseCase _loginUseCase, JwtTokenService _jwtTokenService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateEmployeeRequest request, CancellationToken ct)
    {
        var result =  await _createEmployeeUseCase.createEmployee(request.Name, request.Email, request.YardId, request.Password);
        return CreatedAtAction(nameof(Register), new {id = result.Id}, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var employee = await _loginUseCase.LoginEmployee(request.Email, request.Password, ct);
        if (employee == null) return Unauthorized("Invalid credentials");
        var token = _jwtTokenService.GenerateToken(employee.Email);
        return Ok(token);
        
    }
}