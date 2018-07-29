using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    [HttpGet("{token}")]
    public List<Kategoris> Category(string token)
    {
        var session = HttpContext.Session;
        if (session != null)
        {
            HttpContext.Session.TryGetValue("token", out var result);
            if (result != null)
            {
                string tokenSession = System.Text.Encoding.UTF8.GetString(result).Split('æ')[0];
                if (tokenSession == token)
                {
                    Console.WriteLine("Geçerli token :" + token);
                    using (GazeteContext context = new GazeteContext())
                    {
                        return context.Kategoris.ToList();
                    }
                }
                else
                {
                    Console.WriteLine("Geçerli bir token değil:" + token);
                    //Response.Redirect("https://localhost:5001");
                    return new List<Kategoris>();
                }
            }
            else
            {
                //Response.Redirect("https://localhost:5001");
                return new List<Kategoris>();
            }
        }
        Console.WriteLine("Geçerli bir session yok");
        //Response.Redirect("https://localhost:5001");
        return new List<Kategoris>();
    }
    [HttpGet("{id}/{token}")]
    public List<Habers> CategoryNews(int id, string token)
    {
        var session = HttpContext.Session;
        if (session != null)
        {
            HttpContext.Session.TryGetValue("token", out var result);
            if (result != null)
            {               
                string refreshToken = IsRefreshToken(result);
                string tokenSession = System.Text.Encoding.UTF8.GetString(result).Split('æ')[0];
                if (tokenSession == token)
                {
                    Console.WriteLine("CategoryNews:" + token);
                    using (GazeteContext context = new GazeteContext())
                    {                         
                        var listNews = context.Habers.Where(ha => ha.Kategori_id == id).ToList();
                        if (!String.IsNullOrWhiteSpace(refreshToken))
                        {
                            listNews.ForEach(command =>
                            {
                                command.RefreshToken = refreshToken;
                            });
                        }
                        return listNews;                      
                    }
                }
                else
                {
                    Console.WriteLine("Geçerli bir token değil:" + token);
                    //Response.Redirect("https://localhost:5001");
                    return new List<Habers>();
                }
            }
            else
            {
                //Response.Redirect("https://localhost:5001");
                return new List<Habers>();
            }
        }
        Console.WriteLine("Geçerli bir session yok");
        //Response.Redirect("https://localhost:5001");

        return new List<Habers>();
    }
    [HttpGet("command/{id}/{token}")]
    public List<Yorums> CommandNews(int id, string token)
    {
        var session = HttpContext.Session;
        if (session != null)
        {
            HttpContext.Session.TryGetValue("token", out var result);
            if (result != null)
            {
                string refreshToken = IsRefreshToken(result);
                string tokenSession = System.Text.Encoding.UTF8.GetString(result).Split('æ')[0];
                if (tokenSession == token)
                {
                    Console.WriteLine("CommandNews:" + token);
                    using (GazeteContext context = new GazeteContext())
                    {
                        var listCommand = context.Yorums.Where(yor => yor.Haber_id == id).ToList();
                        if (!String.IsNullOrWhiteSpace(refreshToken))
                        {
                            listCommand.ForEach(command =>
                            {
                                command.RefreshToken = refreshToken;
                            });
                        }
                        return listCommand;
                    }
                }
                else
                {
                    Console.WriteLine("Geçerli bir token değil:" + token);
                    //Response.Redirect("https://localhost:5001");
                    return new List<Yorums>();
                }
            }
            else
            {
                //Response.Redirect("https://localhost:5001");
                return new List<Yorums>();
            }
        }
        Console.WriteLine("Geçerli bir session yok");
        //Response.Redirect("https://localhost:5001");

        return new List<Yorums>();
    }

    public string IsRefreshToken(byte[] token)
    {
        string tokenSession = System.Text.Encoding.UTF8.GetString(token).Split('æ')[1];
        DateTime sessionCreateTime = DateTime.Parse(tokenSession);
        TimeSpan remainingTime = DateTime.Now - sessionCreateTime;
        if (remainingTime.TotalSeconds >= 40)
        {
            string newToken = Guid.NewGuid().ToString()+ "æ" + DateTime.Now;
            HttpContext.Session.Set("token", System.Text.Encoding.UTF8.GetBytes(newToken));
            return newToken;
        }
        return string.Empty;
    }
}