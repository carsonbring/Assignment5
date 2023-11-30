// MusicController.cs

using Microsoft.AspNetCore.Mvc;
using Assignment5.Data;
using Assignment5.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
public class MusicController : Controller
{
    private readonly Assignment5Context _context;

    public MusicController(Assignment5Context context)
    {
        _context = context;
    }
    public IActionResult Index(string? selectedGenre, int? selectedArtistId)
    {
        var distinctGenres = _context.Artist.Select(a => a.Genre).Distinct().ToList();
        ViewBag.Genres = distinctGenres;
        ViewBag.SelectedGenre = selectedGenre;
        ViewBag.ShoppingCart = ShoppingCart.Instance.Songs;
        IEnumerable<Song> songs = null;

        if (!string.IsNullOrEmpty(selectedGenre))
        {
            var artists = _context.Artist.Where(a => a.Genre == selectedGenre).ToList();
            ViewBag.Artists = artists;
            ViewBag.SelectedArtistId = selectedArtistId;
            if (selectedArtistId != null)
            {
                songs = _context.Song
                    .Where(s => s.ArtistId == selectedArtistId)
                    .ToList();
            }
            else
            {
                // If no artist is selected, show all songs for the selected genre
                songs = _context.Song
                    .Where(s => s.Artist.Genre == selectedGenre)
                    .ToList();
            }
        }

        // Other logic...

        return View(songs);
    }
    public IActionResult AddToCart(int songId)
    {
        var song = _context.Song.Where(a => a.Id == songId).Include(s => s.Artist).FirstOrDefault();
        if (song != null)
        {
            if (ShoppingCart.Instance.Songs.Any(s => s.Id == songId))
            {
                TempData["ErrorMessage"] = "This song is already in the shopping cart.";
            }
            else
            {
                ShoppingCart.Instance.Songs.Add(song);
                

            }

        }
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int songId)
    {
        var songToRemove = ShoppingCart.Instance.Songs.FirstOrDefault(s => s.Id == songId);

        if (songToRemove != null)
        {
            ShoppingCart.Instance.Songs.Remove(songToRemove);
            
        }
        else
        {
            // Optionally, you can provide an error message using TempData if the song is not found.
            TempData["ErrorMessage"] = "Song not found in the shopping cart.";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Checkout()
    {
        ShoppingCart.Instance.Songs.Clear();
        TempData["SuccessMessage"] = "Order Successful! Thanks for shopping at MusicShop.com";
        return RedirectToAction("Index");
    }



}
