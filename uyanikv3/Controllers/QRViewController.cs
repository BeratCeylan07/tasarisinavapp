using Microsoft.AspNetCore.Mvc;
using uyanikv3.Models;
using uyanikv3.customModels;

namespace uyanikv3.Controllers;

public class QRViewController : Controller
{
    // GET
    public IActionResult Index(string qrcode)
    {
        if (qrcode is null)
        {
            return RedirectPermanent("https://tasariantalya.com");
        }
        else
        {
            using(var context = new  U1710750DbCb9Context())
            {
                var ogrseansinfo = context.SeansOgrSets.Where(x => x.Qr == qrcode).Select(s => new viewSeansOgrSetModel()
                {
                    Ogr = s.Ogr,
                    kategoriBaslik = s.Seans.Deneme.Kategori.AltKategoriBaslik,
                    denemeAd = s.Seans.Deneme.DenemeBaslik,
                    seansTarih = s.Seans.Tarih.ToString("dd.MM.yyyy"),
                    tarihSaat = s.Seans.Saat,
                    seansyeri = s.Seans.Sinavyeri
                }).FirstOrDefault();
                if (ogrseansinfo is null)
                {
                    return RedirectPermanent("https://tasariantalya.com");
                }
                else
                {
                    ViewBag.SinavAd = ogrseansinfo.denemeAd.ToUpper();
                    ViewBag.OgrAd = ogrseansinfo.Ogr.Ad.ToUpper();
                    ViewBag.OgrSoyad = ogrseansinfo.Ogr.Soyad.ToUpper();
                    ViewBag.seansTarih = ogrseansinfo.seansTarih;
                    ViewBag.seansSaat = ogrseansinfo.tarihSaat;
                    ViewBag.sinavYeri = ogrseansinfo.seansyeri.ToUpper();
                    ViewBag.SinavKategori = ogrseansinfo.kategoriBaslik.ToUpper();
                    ViewBag.QRCODE = qrcode;
                }

            }
            return View();
        }
    }
}