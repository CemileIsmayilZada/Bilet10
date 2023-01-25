using App.Core.Entities;
using App.DataAccess.Repositories.Interfaces;
using AppBusiness.Utilities;
using AppBusiness.ViewModels.TeamMemberrs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AppUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamMemberController : Controller
    {
        private readonly ITeamMemberRepository _repository;
        private readonly IValidator<CreateTeamMemberVM> _validatorCreate;
        private readonly IValidator<UpdateTeamMemberVm> _validatorUpdate;
        private readonly IWebHostEnvironment _env;
        public TeamMemberController(ITeamMemberRepository repository, IWebHostEnvironment env, IValidator<UpdateTeamMemberVm> validatorUpdate, IValidator<CreateTeamMemberVM> validatorCreate)
        {
            _repository = repository;
            _env = env;
            _validatorUpdate = validatorUpdate;
            _validatorCreate = validatorCreate;
        }


        public IActionResult Index()
        {
            return View(_repository.GetAll().AsEnumerable());
        }
        public IActionResult Details(int id)
        {
            return View(_repository.GetById(id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateAsync(CreateTeamMemberVM createTeam)
        {
            string fileName = string.Empty;
            if (createTeam == null) return BadRequest();
            ValidationResult result = await _validatorCreate.ValidateAsync(createTeam);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(createTeam);
            if (createTeam.Image != null)
            {
                if (!createTeam.Image.CheckFileSize(600))
                {
                    ModelState.AddModelError("", "Invalid File Size");
                    return View(createTeam);
                }
                if (!createTeam.Image.CheckFileFormat("image/"))
                {
                    ModelState.AddModelError("", "Invalid File Format");
                    return View(createTeam);
                }
                fileName = createTeam.Image.CopyToFile(_env.WebRootPath, "assets", "img", "team");

            }

            TeamMember member = new()
            {
                Name = createTeam.Name,
                Facebook = createTeam.Facebook,
                Twitter = createTeam.Twitter,
                Instagram = createTeam.Instagram,
                Position = createTeam.Position,
                Image = fileName
            };

            _repository.Create(member);
            await _repository.SaveAsync();
            return RedirectToAction("Index", "TeamMember");
        }

        public IActionResult Update(int id)
        {
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();

            UpdateTeamMemberVm updateTeam = new()
            {
                Facebook = team.Facebook,
                Instagram = team.Instagram,
                Twitter = team.Twitter,
                Position = team.Position,
                Name = team.Name,
                ImagePath = team.Image
            };
            return View(updateTeam);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateAsync(int id, UpdateTeamMemberVm updateTeam)
        {

            if (id != updateTeam.Id) return NotFound();
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();

            string fileName = string.Empty;
            if (updateTeam == null) return BadRequest();

            ValidationResult result = await _validatorUpdate.ValidateAsync(updateTeam);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            if (!ModelState.IsValid) return View(updateTeam);
            if (updateTeam.Image != null)
            {
                if (!updateTeam.Image.CheckFileSize(600))
                {
                    ModelState.AddModelError("", "Invalid File Size");
                    return View(updateTeam);
                }
                if (!updateTeam.Image.CheckFileFormat("image/"))
                {
                    ModelState.AddModelError("", "Invalid File Format");
                    return View(updateTeam);
                }
                fileName = updateTeam.Image.CopyToFile(_env.WebRootPath, "assets", "img", "team");
                team.Image = fileName;
            }

            team.Position = updateTeam.Position;
            team.Name = updateTeam.Name;
            team.Instagram = updateTeam.Instagram;
            team.Facebook = updateTeam.Facebook;
            team.Twitter = updateTeam.Twitter;

            _repository.Update(team);
            await _repository.SaveAsync();

            return RedirectToAction("Index", "TeamMember");
        }

        public IActionResult Delete(int id)
        {
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePostAsync(int id)
        {
            var team = _repository.GetById(id);
            if (team is null) return BadRequest();

            Helper.DeleteFile(_env.WebRootPath, "assets", "img", "team", team.Image);

            _repository.Delete(team);
            await _repository.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
