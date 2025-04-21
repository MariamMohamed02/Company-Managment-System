using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Company.Project.PresentationLayer.Helpers;
using Company.Project.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Project.PresentationLayer.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
              var role= await  _roleManager.FindByNameAsync(model.Name);
                if (role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,
                    };

                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(u => new RoleToReturnDto()
                {
                   Id= u.Id,
                   Name= u.Name,
                }
                 );
            }
            else
            {
                roles = _roleManager.Roles.Select(u => new RoleToReturnDto()
                {
                    Id = u.Id,
                    Name = u.Name,
                }
                 ).Where(u => u.Name.ToLower().Contains(SearchInput));
            }

            return View(roles);
        }




        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid ID");
            var user = await _roleManager.FindByIdAsync(id);
            if (user is null) return NotFound(new { statusCode = 404, message = $"Role with Id : {id} is not found" });
            var dto = new RoleToReturnDto()
            {
                Id = user.Id,
               Name=user.Name,
            };

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)// Action to go to the view of the Update 
        {


            // Return the Deatails Action (not the View Action)
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model) // Action to go to submit the updated values 
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation");
                var user = await _roleManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invalid Operation");
                var userResult = await _roleManager.FindByNameAsync(model.Name);

                if (userResult is  null)
                {
                    user.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }

                ModelState.AddModelError("", "Invaid Operation");
                



            }
            return View(model);
        }







        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {

            return await Details(id, "Delete");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            // Handle failure
            ModelState.AddModelError("", "Failed to delete the role.");

            var dto = new RoleToReturnDto
            {
                Id = role.Id,
                Name = role.Name
            };
            return View("Delete", dto);
        }


        [HttpGet]
        // view will display all users therefore we need the userManager service because it has all the users
        public async Task< IActionResult >AddOrRemoveUser( string roleId)
        {
           var role= await _roleManager.FindByIdAsync(roleId);
            if (role is null) return NotFound();

            ViewData["RoleId"]=roleId;
            var usersInRole = new List<UsersInRoleViewModel>();
            var users= await _userManager.Users.ToListAsync();
            foreach (var user in users) {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };

                if (await _userManager.IsInRoleAsync(user, role.Name)) {

                    userInRole.IsSelected=true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                usersInRole.Add(userInRole);
            }


            return View(usersInRole);

        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UsersInRoleViewModel> users)
        {
            
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }
                }
                return RedirectToAction(nameof(Edit), new { id = roleId });
            }

            return View(users);
        }


    }
}
