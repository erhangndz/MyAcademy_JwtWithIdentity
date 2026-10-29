using JwtProject.API.DTOs.RoleDtos;
using JwtProject.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace JwtProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController(RoleManager<AppRole> _roleManager,
                                 UserManager<AppUser> _userManager) : ControllerBase
    {

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDto roleDto)
        {
            //var role = new AppRole
            //{
            //    Name = roleDto.Name
            //};
            var result = await _roleManager.CreateAsync(new AppRole { Name = roleDto.Name });

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Rol başarıyla oluşturuldu");
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole(AssignRoleDto roleDto)
        {
            var user = await _userManager.FindByNameAsync(roleDto.UserName);
            if(user is null)
            {
                return BadRequest("Kullanıcı bulunamadı");
            }

            var result = await _userManager.AddToRoleAsync(user, roleDto.RoleName);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Kullanıcıya rol atandı");
        }
    }
}
