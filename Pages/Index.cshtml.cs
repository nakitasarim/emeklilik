using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Emeklilik.Pages;

public class IndexModel : PageModel
{
    [BindProperty]
    public DateTime IseBaslamaTarihi { get; set;}  
    [BindProperty]  
    public DateTime DogumTarihi { get; set;}
    [BindProperty]
    public string Gender { get; set; } 
    [BindProperty]   
    public int GunSayisi99Oncesi { get; set; } 
    [BindProperty]  
    public int AySayisiFHZ2002Oncesi { get; set; }     
    public string Sonuc { get; set; }         
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        Sonuc = "Devlet Memuru emeklilik yaşı için üç önemli tarih bulunmaktadır. Bunlar:\n\r" +
                "    08.09.1999 tarihinde çıkarılan yasa ile emeklilik için yaş şartı da getirildi. Bu tarihten önce kadınlar için 20, erkekler için 25 hizmet yılını doldurmak emekli olmak için yetiyordu.Bu yasa ile kadın için 58, erkekler için 60 yaş şartı getirildi.\n\r" +
                "    23.05.2002 tarihinde 08.09.1999 tarihinden önce işe başlayanlar için kademeli yaş getirildi.\n\r" +
                "    01.05.2008 tarihinde ileriye yönelik kademeli olarak emeklilik yaşı 65 yapıldı.\n\r" +
                "23.05.2002 tarihinden önceki Fiili Hizmet Zammı(FHZ) süreleri emeklilik yaşınan doğrudan düşülür.Örneğin askerlik döneminde 3 ay FHZ kazanmışsanız, emeklilik tarihiniz 3 ay erkene alınır.\n\r";
    }

    public void OnGet()
    {

    }

    public void OnPost()
    {
        var islem = new Hesaplamalar();
        islem.IseBaslamaTarihi = IseBaslamaTarihi;
        islem.DogumTarihi = DogumTarihi;
        if(Gender == "Erkek")
            islem.ErkekMi = true;
        else
            islem.ErkekMi = false;        
        islem.GunSayisi99Oncesi = GunSayisi99Oncesi;
        islem.AySayisiFHZ2002Oncesi = AySayisiFHZ2002Oncesi;
        Sonuc = islem.HesaplaEmeklilikYasi();
    }
}
