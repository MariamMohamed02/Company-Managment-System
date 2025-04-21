using Company.Project.DataLayer.Models;
using Company.Project.PresentationLayer.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Project.PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }
        
        
        
        
        
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<UserToReturnDto> users;
            if (string.IsNullOrEmpty(SearchInput))
            {
               users= _userManager.Users.Select(u=> new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName= u.UserName,
                    Email=u.Email,
                    FirstName=u.FirstName,
                    LastName=u.LastName,
                    Roles=_userManager.GetRolesAsync(u).Result,
                }
                );
            }
            else
            {
                users = _userManager.Users.Select(u => new UserToReturnDto()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Roles = _userManager.GetRolesAsync(u).Result,
                }
            ).Where(u=>u.FirstName.ToLower().Contains(SearchInput));
            }

            return View(users);
        }




        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid ID");
            var user = await _userManager.FindByIdAsync(id);
            if (user is null) return NotFound(new { statusCode = 404, message = $"Employee with Id : {id} is not found" });
            var dto = new UserToReturnDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = _userManager.GetRolesAsync(user).Result,
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
        public async Task<IActionResult> Edit([FromRoute] string id, UserToReturnDto model) // Action to go to submit the updated values 
        {

            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Operation");
                var user =await _userManager.FindByIdAsync(id);
                if (user is null) return BadRequest("Invalid Operation");
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result= await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

               

            }
            return View(model);
        }







        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            
            return await Details(id,"Delete");
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id, UserToReturnDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id != model.Id) return BadRequest("Invalid Operation");
        //        var user = await _userManager.FindByIdAsync(id);
        //        if (user is null) return BadRequest("Invalid Operation");

        //        var result = await _userManager.DeleteAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }



        //    }
        //    return View(model);
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            // Handle failure
            ModelState.AddModelError("", "Failed to delete the user.");
            var dto = new UserToReturnDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = await _userManager.GetRolesAsync(user)
            };
            return View("Delete", dto);
        }



    }
}
