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
        //Setting ViewBag Variables to be used in the View
        var distinctGenres = _context.Artist.Select(a => a.Genre).Distinct().ToList();
        ViewBag.Genres = distinctGenres;
        ViewBag.SelectedGenre = selectedGenre;
        ViewBag.ShoppingCart = ShoppingCart.Instance.Songs;
        IEnumerable<Song> songs = null;
        //Selecting artists to display based on the selected genre
        if (!string.IsNullOrEmpty(selectedGenre))
        {
            var artists = _context.Artist.Where(a => a.Genre == selectedGenre).ToList();
            ViewBag.Artists = artists;
            ViewBag.SelectedArtistId = selectedArtistId;
            //Selecting the songs to display based on the selected artist
            if (selectedArtistId != null)
            {
                songs = _context.Song
                    .Where(s => s.ArtistId == selectedArtistId)
                    .ToList();
            }
            else
            {
                //If no artist is selected, show all songs for the selected genre
                songs = _context.Song
                    .Where(s => s.Artist.Genre == selectedGenre)
                    .ToList();
            }
        }

        return View(songs);
    }
    public IActionResult AddToCart(int songId)
    {
        //Selectig the song where the ID is equal to the songId that is passed into the Asp-Action on the view
        var song = _context.Song.Where(a => a.Id == songId).Include(s => s.Artist).FirstOrDefault();
       
        if (song != null)
        {
            //If the song is already in the shopping cart, display an error message
            if (ShoppingCart.Instance.Songs.Any(s => s.Id == songId))
            {
                TempData["ErrorMessage"] = "This song is already in the shopping cart.";
            }
            else
            {
                //else, add the song to the shopping cart
                ShoppingCart.Instance.Songs.Add(song);
                

            }

        }
        return RedirectToAction("Index");
    }

    public IActionResult RemoveFromCart(int songId)
    {
        //selectign the song to remove form the passed in song id
        var songToRemove = ShoppingCart.Instance.Songs.FirstOrDefault(s => s.Id == songId);

        if (songToRemove != null)
        {
            ShoppingCart.Instance.Songs.Remove(songToRemove);
            
        }
        else
        {
            //print error message 
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
