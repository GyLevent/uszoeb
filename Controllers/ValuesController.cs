using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using uszoeb_Gyurko_Levente_backend.Models; 

[Route("api/[controller]")]
[ApiController]
public class VersenyzokController : ControllerBase
{
    private readonly UszoebContext _context; 

    public VersenyzokController(UszoebContext context)
    {
        _context = context;
    }

    private const string UID = "FEB3F4FEA09CE43E";

    [HttpGet]
    public IActionResult GetVersenyzokWithOrszagAndSzamok()
    {
        var versenyzokData = _context.Versenyzoks
            .Include(v => v.Orszag) 
            .Include(v => v.Szamoks) 
            .Select(v => new
            {
                VersenyzoId = v.Id,
                VersenyzoNev = v.Nev,
                OrszagNev = v.Orszag.Nev, 
                Versenyszamok = v.Szamok.Select(s => s.Nev).ToList() 
            })
            .ToList();

        return Ok(versenyzokData);
    }
    [HttpGet]
    public IActionResult GetVersenyszamok()
    {
        var versenyszamok = _context.Szamoks
            .Select(s => new
            {
                SzamId = s.Id,
                SzamNev = s.Nev
            })
            .ToList();

        return Ok(versenyszamok);
    }

    [HttpPost]
    [Route("AddVersenyzok")]
    public IActionResult AddVersenyzok(string userID, [FromBody] Versenyzok versenyzo)
    {
        if (!IsUserAuthenticated(userID))
        {
            return Unauthorized("Nincs jogosultsága új versenyző felvételéhez!");
        }

        try
        {
            _context.Versenyzoks.Add(versenyzo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetVersenyzok), new { id = versenyzo.Id }, "Versenyző hozzáadása sikeresen megtörtént.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba történt a versenyző hozzáadása közben: {ex.Message}");
        }
    }
    [HttpPut]
    [Route("UpdateVersenyzo/{id}")]
    public IActionResult UpdateVersenyzo(string userID, int id, [FromBody] Versenyzok updatedVersenyzoData)
    {
        if (!IsUserAuthenticated(userID))
        {
            return Unauthorized("Nincs jogosultsága a versenyző adatainak módosításához!");
        }

        try
        {
            var existingVersenyzo = _context.Versenyzoks.Find(id);

            if (existingVersenyzo == null)
            {
                return NotFound("A megadott azonosítójú versenyző nem található.");
            }

            existingVersenyzo.Nev = updatedVersenyzoData.Nev;
            existingVersenyzo.OrszagId = updatedVersenyzoData.OrszagId;
            existingVersenyzo.Nem = updatedVersenyzoData.Nem;

            _context.SaveChanges();

            return Ok("Versenyző adatainak módosítása sikeresen megtörtént.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba történt a versenyző adatainak módosítása közben: {ex.Message}");
        }
    }
    [HttpDelete]
    [Route("DeleteVersenyzo/{id}")]
    public IActionResult DeleteVersenyzo(string userID, int id)
    {
        if (!IsUserAuthenticated(userID))
        {
            return Unauthorized("Nincs jogosultsága a versenyzők adatainak a törléséhez!");
        }

        try
        {
            var versenyzo = _context.Versenyzoks.Find(id);

            if (versenyzo == null)
            {
                return NotFound("Nem található a megadott azonosítóval rendelkező versenyző.");
            }

            _context.Versenyzoks.Remove(versenyzo);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba történt a versenyző törlése közben: {ex.Message}");
        }
    }


    private bool IsUserAuthenticated(string userID)
    {
        return userID == UID;
    }
}
