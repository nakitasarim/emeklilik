public class Hesaplamalar //4c Memurlar için
    {
        public DateTime DogumTarihi { get; set; }
        public DateTime IseBaslamaTarihi { get; set; }
        public DateTime EmekliOlmaTarihi { get; set; }//işe başlama tarihine göre ara vermezse 25 yıl yada 9000 günü dolruma tarihi
        public int GunSayisi99Oncesi { get; set; }//23.05.2002 tarihinden önce
        public int AySayisiFHZ2002Oncesi { get; set; }//23.05.2002 önces kazanılan Fiili  Hizmet Zamları
        public bool ErkekMi { get; set; } //cinsiyet

        internal int String2Int(string str)
        {
            try
            {
                int x = Int32.Parse(str);
                return x; 
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is invalid.");

                return 0;
            }
        }

        internal string HesaplaEmeklilikYasi() {
            DateTime date_08091999 = new DateTime(1999, 09, 08);
            DateTime date_01052008 = new DateTime(2008, 05, 01);
            if (IseBaslamaTarihi < date_08091999) return Hesapla99OncesiKademe();
            else if (IseBaslamaTarihi > date_08091999 & IseBaslamaTarihi <= date_01052008) return Hesapla2008Oncesi();
            else if (IseBaslamaTarihi > date_01052008) return Hesapla2008Sonrasi();
            return "Hesaplama için; doğum tarihini ve işe başlama tarihini girmelisiniz!";
        }

        private string Hesapla2008Sonrasi()
        {
            int emeklilikYasi = 60;
            DateTime date_31122008 = new DateTime(2008, 12, 31);//01.05.2008 ile 31.12.2008 e kadar erkekler için 60 kadınlar için 58
            if (ErkekMi)
            {
                if (IseBaslamaTarihi > date_31122008)                   
                    emeklilikYasi = Hesapla2008SonrasiEmekliOlmaYasiErkek();
            }
            else
            {
                if (IseBaslamaTarihi < date_31122008) emeklilikYasi = 58;//buraya zaten 01.05.2008 den sonrakiler geliyor.
                else emeklilikYasi = Hesapla2008SonrasiEmekliOlmaYasiKadin();
            }
            return "01.05.2008 tarihinden sonra işe başladığınız için 25 yıl kesintisisiz çalışırsanız emeklilik yaşınız: " + emeklilikYasi+ 
                "\n\r Emeklilik tarihiniz: " +HesaplaEmekliOlunacakTarih(emeklilikYasi) + " olacaktır.";
        }

        private string HesaplaEmekliOlunacakTarih(int emeklilikYasi)
        {
            string sonuc = (DogumTarihi.AddYears(emeklilikYasi).AddMonths(-AySayisiFHZ2002Oncesi)).ToShortDateString();
            return sonuc;
        }

        private int Hesapla2008SonrasiEmekliOlmaYasiErkek()
        {
            //25 yıl doldurma tarihi
            DateTime date_25yilSonra = IseBaslamaTarihi.AddYears(25);
            //Console.Write("date_25yilSonra : "+date_25yilSonra);
            if ((date_25yilSonra > (new DateTime(2036, 1, 1)) & (date_25yilSonra < (new DateTime(2037, 12, 31))))) return 61;
            if ((date_25yilSonra > (new DateTime(2038, 1, 1)) & (date_25yilSonra < (new DateTime(2039, 12, 31))))) return 62;
            if ((date_25yilSonra > (new DateTime(2040, 1, 1)) & (date_25yilSonra < (new DateTime(2041, 12, 31))))) return 63;
            if ((date_25yilSonra > (new DateTime(2042, 1, 1)) & (date_25yilSonra < (new DateTime(2043, 12, 31))))) return 64;
            if ((date_25yilSonra > (new DateTime(2044, 1, 1)) & (date_25yilSonra < (new DateTime(2045, 12, 31))))) return 65;
            if ((date_25yilSonra > (new DateTime(2046, 1, 1)) & (date_25yilSonra < (new DateTime(2047, 12, 31))))) return 65;
            return 60;
        }

        private int Hesapla2008SonrasiEmekliOlmaYasiKadin()
        {
            //25 yıl doldurma tarihi
            DateTime date_25yilSonra = IseBaslamaTarihi.AddYears(25);
            if ((date_25yilSonra > (new DateTime(2036, 1, 1)) & (date_25yilSonra < (new DateTime(2037, 12, 31))))) return 59;
            if ((date_25yilSonra > (new DateTime(2038, 1, 1)) & (date_25yilSonra < (new DateTime(2039, 12, 31))))) return 60;
            if ((date_25yilSonra > (new DateTime(2040, 1, 1)) & (date_25yilSonra < (new DateTime(2041, 12, 31))))) return 61;
            if ((date_25yilSonra > (new DateTime(2042, 1, 1)) & (date_25yilSonra < (new DateTime(2043, 12, 31))))) return 62;
            if ((date_25yilSonra > (new DateTime(2044, 1, 1)) & (date_25yilSonra < (new DateTime(2045, 12, 31))))) return 63;
            if ((date_25yilSonra > (new DateTime(2046, 1, 1)) & (date_25yilSonra < (new DateTime(2047, 12, 31))))) return 64;
            return 0;
        }

        private string Hesapla2008Oncesi()
        {
            int emeklilikYasi = 60;
            if (!ErkekMi) emeklilikYasi = 58;//kadınsa emeklilik yaşı 58 oluyor
            return "08.09.1999 ile 01.05.2008 tarihleri arası işe başladığınız için emeklilik yaşınız " + emeklilikYasi + "\n\r" +
                "Emeklili tarihiniz : " + HesaplaEmekliOlunacakTarih(emeklilikYasi);
        }

        internal string Hesapla99OncesiKademe()
        {
            ///23.05.2002 tarihindeki hizmetini hesaplıyoruz
            string sonuc = "";
            //08.09.1999 tarihi öncesi işe başmamış mı?
            sonuc = "08.09.1999 öncesi işe başlamışsınız\n\r";
            //else sonuc = "08.09.1999 sonrası işe başlamışsınız.\n\r";
            //Cinsiyet
            string cinsiyet = "Erkek";
            int emeklilikYasi = 0, 
                emeklilikYasiEytli= (IseBaslamaTarihi.Year - DogumTarihi.Year); //işe başladığı tarigte kaç yaında onu buluyoruz.
   
            if (ErkekMi)
            {
                emeklilikYasi = EmeklilikYasi99OncesiErkek();
                emeklilikYasiEytli += 25;
            }
            else
            {
                cinsiyet = "Kadın";
                emeklilikYasi = EmeklilikYasi99OncesiKadin();
                emeklilikYasiEytli += 20;
            }
            //sonuc += "Cinsiyet :" + cinsiyet + "\n\r";
            sonuc += "Sosyal güvenlik mevzuatına göre; kural olarak ilgili ayın kaç çektiği önemsizdir. Her ay 30 gün, yıl 360 gün kabul edilmektedir."+Environment.NewLine;
            //99 öncesi hizmeti yıl ay gün

            sonuc += "23.05.2002 öncesi hizmet günü: " + this.GunSayisi99Oncesi + " EYT'siz kademeli emeklilik yaşı: "+ emeklilikYasi+ Environment.NewLine;
            sonuc += "EYT'siz emeklilik tarihiniz : " + HesaplaEmekliOlunacakTarih(emeklilikYasi) +" ancak EYT'li olduğunuz için Emeklilik tarihiniz : " + HesaplaEmekliOlunacakTarih(emeklilikYasiEytli);

            return sonuc;
        }

        private int EmeklilikYasi99OncesiErkek()
        {
            //23.05.2002 tarihindeki hizmet yılına göre emeklilik yaşı
            //Min     Max     Emeklilik Yaşı
            if (GunSayisi99Oncesi >= 9000) return 44;
            if (GunSayisi99Oncesi >= 8280) return 44;
            if (GunSayisi99Oncesi >= 7740 & GunSayisi99Oncesi <= 8279) return 45;
            if (GunSayisi99Oncesi >= 7200 & GunSayisi99Oncesi <= 7739) return 46; //        
            if (GunSayisi99Oncesi >= 6660 & GunSayisi99Oncesi <= 7199) return 47; //        
            if (GunSayisi99Oncesi >= 6120 & GunSayisi99Oncesi <= 6659) return 48; //        
            if (GunSayisi99Oncesi >= 5580 & GunSayisi99Oncesi <= 6119) return 49; //        
            if (GunSayisi99Oncesi >= 5040 & GunSayisi99Oncesi <= 5579) return 50; //        
            if (GunSayisi99Oncesi >= 4500 & GunSayisi99Oncesi <= 5039) return 51; //        
            if (GunSayisi99Oncesi >= 3960 & GunSayisi99Oncesi <= 4499) return 52; //        
            if (GunSayisi99Oncesi >= 3420 & GunSayisi99Oncesi <= 3959) return 53; //        
            if (GunSayisi99Oncesi >= 2880 & GunSayisi99Oncesi <= 3419) return 54; //        
            if (GunSayisi99Oncesi >= 2340 & GunSayisi99Oncesi <= 2879) return 55; //        
            if (GunSayisi99Oncesi >= 1800 & GunSayisi99Oncesi <= 2339) return 56; //        
            if (GunSayisi99Oncesi >= 1260 & GunSayisi99Oncesi <= 1799) return 57; //        
            if (GunSayisi99Oncesi >= 1080 & GunSayisi99Oncesi <= 1259) return 58; //        
            return 60;
        }

        private int EmeklilikYasi99OncesiKadin()
        {
            //23.05.2002 tarihindeki hizmet yılına göre emeklilik yaşı
            //Min     Max     Kadın Emeklilik Yaşı
            if (GunSayisi99Oncesi >= 7200) return 40;
            if (GunSayisi99Oncesi >= 6480) return 40;
            if (GunSayisi99Oncesi >= 6120 & GunSayisi99Oncesi <= 6479) return 41; //		
            if (GunSayisi99Oncesi >= 5760 & GunSayisi99Oncesi <= 6119) return 42; // 		
            if (GunSayisi99Oncesi >= 5400 & GunSayisi99Oncesi <= 5759) return 43; // 		
            if (GunSayisi99Oncesi >= 5040 & GunSayisi99Oncesi <= 5399) return 44; // 		
            if (GunSayisi99Oncesi >= 4680 & GunSayisi99Oncesi <= 5039) return 45; // 		
            if (GunSayisi99Oncesi >= 4320 & GunSayisi99Oncesi <= 4679) return 46; // 		
            if (GunSayisi99Oncesi >= 3960 & GunSayisi99Oncesi <= 4319) return 47; // 		
            if (GunSayisi99Oncesi >= 3600 & GunSayisi99Oncesi <= 3959) return 48; // 		
            if (GunSayisi99Oncesi >= 3240 & GunSayisi99Oncesi <= 3599) return 49; // 		
            if (GunSayisi99Oncesi >= 2880 & GunSayisi99Oncesi <= 3239) return 50; // 		
            if (GunSayisi99Oncesi >= 2520 & GunSayisi99Oncesi <= 2879) return 51; // 		
            if (GunSayisi99Oncesi >= 2160 & GunSayisi99Oncesi <= 2519) return 52; // 		
            if (GunSayisi99Oncesi >= 1800 & GunSayisi99Oncesi <= 2159) return 53; //  		
            if (GunSayisi99Oncesi >= 1440 & GunSayisi99Oncesi <= 1799) return 54; //  		
            if (GunSayisi99Oncesi >= 1080 & GunSayisi99Oncesi <= 1439) return 55; //		

            return 58;
        }
    }