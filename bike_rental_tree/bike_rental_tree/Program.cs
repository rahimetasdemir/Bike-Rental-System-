using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_Proje3_Tree
{
    public class Program
    {
        public class DurakSınıfı // durak sınıfı

        {
            public string durakAdi;
            public int bosPark;
            public int tandemBisiklet;
            public int normalBisiklet;
            public List<MusteriSınıfı> durak_musterileri; // durak nesnesi durağa ait müşterilerin bulunduğu listeyi de içerecek

            public DurakSınıfı(string durakAdi, int bosPark, int tandemBisiklet, int normalBisiklet) // içerisinde müşteriler oladığında kullanılacak constructor metot
            {
                this.durakAdi = durakAdi;
                this.bosPark = bosPark;
                this.tandemBisiklet = tandemBisiklet;
                this.normalBisiklet = normalBisiklet;
            }

            public DurakSınıfı(string durakAdi, int bosPark, int tandemBisiklet, int normalBisiklet, List<MusteriSınıfı> durak_musterileri) // müşterileri de durak nesnesi içerisine dahil etmek için constructor metot
            {
                this.durakAdi = durakAdi;
                this.bosPark = bosPark;
                this.tandemBisiklet = tandemBisiklet;
                this.normalBisiklet = normalBisiklet;
                this.durak_musterileri = durak_musterileri;

            }
        }

        public class MusteriSınıfı // müşteri sınıfı
        {
            public int ID;
            public string kiralama_saati;

            public MusteriSınıfı(int ID, string kiralama_saati) // constructor metot
            {
                this.ID = ID;
                this.kiralama_saati = kiralama_saati;
            }
        }

        public class TreeNode
        {
            public DurakSınıfı data; //ağaçta durak sınıfına ait nesneler yer alacak
            public TreeNode leftChild;
            public TreeNode rightChild;
            public void displayNode()
            { Console.WriteLine("DURAK ADI: " + data.durakAdi + " BP: " + data.bosPark + " TB: " + data.tandemBisiklet + " NB: " + data.normalBisiklet + " "); }
        }


        public class Tree // ağaç sınıfı
        {
            private TreeNode root;
            public int sayi;
            public Tree() { root = null; }
            public TreeNode getRoot()
            { return root; }

            // Agacın inOrder Dolasılması
            public void inOrder(TreeNode localRoot)
            {
                if (localRoot != null)
                {
                    inOrder(localRoot.leftChild);
                    localRoot.displayNode();
                    inOrder(localRoot.rightChild);
                }
            }

            // Ağaca bir düğüm eklemeyi sağlayan metot
            public void insert(DurakSınıfı newdata)
            {
                TreeNode newNode = new TreeNode();
                newNode.data = newdata;  // ağacın düğümü DurakSınıfı ile oluşturulan nesneleri içerecek
                if (root == null)
                    root = newNode;
                else
                {
                    TreeNode current = root;
                    TreeNode parent;
                    while (true)
                    {
                        parent = current;
                        int compare = newdata.durakAdi.CompareTo(current.data.durakAdi); // compareTo -1, 0 veya 1 döndürecek
                        if (compare < 0)// compare strings
                        {
                            current = current.leftChild;
                            if (current == null)
                            {
                                parent.leftChild = newNode;
                                return;
                            }
                        }
                       
                        else
                        {
                            current = current.rightChild;
                            if (current == null)
                            {
                                parent.rightChild = newNode;
                                return;
                            }
                        }
                    } // end while

                }

            }
        }

        
        public static List<MusteriSınıfı> Durak_musteri_listesi_Olustur(int eklenecek_musteri_sayısı ) // duraklara ait müşteri listesini oluşturan metot
        {
            
            List<MusteriSınıfı> Durak_musterileri = new List<MusteriSınıfı>(eklenecek_musteri_sayısı);// müşteri sayısı uzunluğunda liste için bellekten yer ayrılıyor
            for (int i = 0; i < eklenecek_musteri_sayısı; i++)
            {
                int random_ID = random.Next(1, 21);
                string randomSaat = Convert.ToString(random.Next(6, 23));
                string randomDakika = Convert.ToString(random.Next(0, 59));
                string saatBilgisi = randomSaat + ":" + randomDakika;
                MusteriSınıfı musteri = new MusteriSınıfı(random_ID, saatBilgisi);// müşteri nesnesi oluşturuluyor
                Durak_musterileri.Add(musteri);// oluşturulan müşteri duraktaki müşterilerin olduğu listeye ekleniyor
                 
            }
            return Durak_musterileri;
        }


        
        static int maxDepth = 0;
        private static void Agac_derinligi_bul_bilgileri_yaz(TreeNode node, int depth) // ağacı dolaşarak ağacın derinliğini ve kiralama yapan müşterilerin bilgilerini bulan metod
        {   
            //preorder olarak dolaşılıp bilgiler yazdırılıyor
            if (node != null)
            {
                depth++;

                int kiralama_yapan_musteri_sayisi = node.data.durak_musterileri.Count;// durakta kaç tane müşterinin kiralama yaptığı hesaplanıyor

                node.displayNode();// durak bilgileri ekrana yazılıyor
                Console.WriteLine("Durakta kiralama yapan müşteri sayısı: " + kiralama_yapan_musteri_sayisi);
                Console.WriteLine("Durak Müşterileri: ");

                foreach (MusteriSınıfı item in node.data.durak_musterileri)
                {

                    Console.WriteLine("müşteri ID: " + item.ID + "  müşteri kiralama saati:  " + item.kiralama_saati);

                }

                if (depth > maxDepth) // bulumduğum düğümün derinliği ile daha önceden uğramış olduğum maksimum derinliği karşılaştırıyor 
                    maxDepth = depth; // maxDepth güncelleniyor
                Console.WriteLine();
                Agac_derinligi_bul_bilgileri_yaz(node.leftChild, depth); //sol sub-tree dolaşılıyor
                Agac_derinligi_bul_bilgileri_yaz(node.rightChild, depth); //sağ sub-tree dolaşılıyor

            }

        }

        private static void Belirtilen_musterinin_kiralama_yaptığı_saatler(TreeNode durak_dugumu, int klavye_musteriID)  // klavyedeki bilgilere ait müşteriyi ağaçta arayarak kiralama yaptığı saatleri bulan metot
        {
            if (durak_dugumu != null)
            {
                string[] Musterinin_kiralama_yaptigi_saatler = new string[durak_dugumu.data.durak_musterileri.Count]; // belirtilen müşterinin kiralama yaptığı saatler bu dizide tutulacak (müşterinin aynı durakta birde fazla kiralama yapmış olma ihtimali olduğu için)
                int j = 0;
                foreach (MusteriSınıfı musteri in durak_dugumu.data.durak_musterileri)
                {

                    if (klavye_musteriID == musteri.ID)
                    {
                        Musterinin_kiralama_yaptigi_saatler[j] = musteri.kiralama_saati; // belirtilen ID ye sahip müşterinin kiralama yaptığı saat diziye atıldı
                        j++; // index güncellerniyor
                       

                    }

                }

                Console.WriteLine(durak_dugumu.data.durakAdi + " durağındaki  kiralama saatleri : ");

                if (Musterinin_kiralama_yaptigi_saatler[0] == null) // eğer müşteri o durakta kiralama yapmamışsa dizi boştur
                  
                {
                 Console.WriteLine(" Belirtilen ID numarasına sahip müşteri bu durakta kiralama yapmamıştır");
                 Console.WriteLine();
                }
                else
                {
                    foreach (string item in Musterinin_kiralama_yaptigi_saatler)
                    {
                        Console.WriteLine(item);
                    }

                }

                // aynı işlemler sol ve sağ alt ağaç için de gerçekleştirilecek
                Belirtilen_musterinin_kiralama_yaptığı_saatler(durak_dugumu.leftChild, klavye_musteriID); //sol sub-tree dolaşılıyor
                Belirtilen_musterinin_kiralama_yaptığı_saatler(durak_dugumu.rightChild, klavye_musteriID); //sağ sub-tree dolaşılıyor
               
            }

           
        }

        // Klavyede bilgileri girilen müşteri için normal  bisiklet kiralama işlemi yapılıyor
        public static void Belirtilen_Durak_ve_ID_icin_kiralama_islemi(TreeNode durak_dugumu, int klavye_yeni_musteriID, string klavye_durak_adi)
        {
            if (durak_dugumu != null)
            {
                int compare = klavye_durak_adi.CompareTo(durak_dugumu.data.durakAdi); // stringlerin compareTo metodu ile karşılaştırılması sonucu döndürülen değeri tutacak
                if (compare == 0)
                {   // klavyede belirtilen durak ağacı dolaşarak bulunduğunda verilen ID ve üretilen random saate göre yeni müşteri nesnesi oluşturulup listeye ekleniyor
                    
                    string yeni_randomSaat = Convert.ToString(random.Next(6, 23)); // sistemin 06:00-23:00 saatleri arasında açık olduğu varsayılarak
                    string yeni_randomDakika = Convert.ToString(random.Next(0, 59));
                    string yeni_saatBilgisi = yeni_randomSaat + ":" + yeni_randomDakika;

                    MusteriSınıfı yeni_musteri = new MusteriSınıfı(klavye_yeni_musteriID, yeni_saatBilgisi);
                    durak_dugumu.data.durak_musterileri.Add(yeni_musteri);// kiralama yapan müşteri listeye eklendi
                    durak_dugumu.data.bosPark += 1;
                    durak_dugumu.data.normalBisiklet -= 1;   // boş park ve normal bisiklet sayısı güncelleniyor

                    Console.WriteLine("Kiralama işleminden sonra ilgili durağa ait bilgiler:");
                    Console.Write("DURAK ADI: "+durak_dugumu.data.durakAdi+ " BP: " + durak_dugumu.data.bosPark + " TB: " + durak_dugumu.data.tandemBisiklet+ " NB: " + durak_dugumu.data.normalBisiklet);
                }


                // recursive olarak aynı işlemler sağ ve sol alt ağaçlar için de tekrar edilip ağaç dolaşılarak gerekli işlemler yapılıyor
                Belirtilen_Durak_ve_ID_icin_kiralama_islemi(durak_dugumu.leftChild, klavye_yeni_musteriID,klavye_durak_adi); //sol sub-tree dolaşılıyor
                Belirtilen_Durak_ve_ID_icin_kiralama_islemi(durak_dugumu.rightChild, klavye_yeni_musteriID,klavye_durak_adi); //sağ sub-tree dolaşılıyor
            }

             
        }


        static Random random = new Random(); // rastgele üretilen sayıya göre duraklarda bulunan müşteri listesi oluşturulacak

        public static void Main(string[] args)
        {

            // yeni eklenen duraklar:"Susuzdede,9,16,2", "Konak İskele,13,25,5", "Köprü,17,18,3","Kuş Cenneti,30,11,4", "Bornova Metro,6,16,9"
            // bisiklet sisteminde yer alan duraklar ve içerisinde bulunan bisiklet ve boş park sayıları "duraklar " listesinde tutulmaktadır.
            String[] duraklar = { "İnciraltı, 28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Bostanlı İskele, 7, 0, 5", "Susuzdede,9,16,2", "Konak İskele,13,25,5", "Köprü,17,18,3", "Kuş Cenneti,30,11,4", "Bornova Metro,6,16,9" };

            Tree ikiliAramaAgaci = new Tree(); // Tree sınıfından boş bir ağaç yapısı oluşturuluyor

            for (int i = 0; i < duraklar.Length; i++)
            {
                int random1 = random.Next(1, 11);// listede bulunacak müşteri sayısı random olarak üretiliyor

                int duraga_eklenecek_musteri_say=1; // Durakta bulunan müşteri listesinde kaç müşteri olacağını tutan değişken(1-10 arasında müşteri ekleneceğiiçin en az 1değerini verdim)
                int kiralanan_tandem_bisiklet=0; // Duraktaki normal bisiklet sayısı yeterli olmadığında tandem bisikletten kiralama yapılacak sayıyı tutan değişken
                int Kiralanan_normal_bisiklet=0; // kiralanan normal bisiklet sayısını tutacak değişken

                string durak_bilgisi = duraklar[i]; // duraklar dizisindeki her bir durağın bilgilerini tutan string
                string[] Bilgiler = durak_bilgisi.Split(','); // durak bilgileri virgüllerden ayrılıyor
                string durak_adi = Bilgiler[0]; // virgül ile ayrılan bilgiler durak nesnesini oluşturmak için gerekli değişkenlere atanıyor
                int bos_park = Convert.ToInt32(Bilgiler[1]);
                int tandem_bisiklet = Convert.ToInt32(Bilgiler[2]);
                int normal_bisiklet = Convert.ToInt32(Bilgiler[3]);

                DurakSınıfı durak_nesnesi = new DurakSınıfı(durak_adi, bos_park, tandem_bisiklet, normal_bisiklet); // durak nesnesi oluşturuluyor

                // Durağa müşteriler eklenirken aşağıda yer alan kontroller sağlanıp müşteriler duraktaki müşteri listesine eklendi
                
                int kullanılabilecek_bisiklet = durak_nesnesi.normalBisiklet+durak_nesnesi.tandemBisiklet-1;  // bir duraktan normal bisiklet kiralayabilmek için toplamı bulurken normal bisikletlerin 1 eksiğini aldım(tüm normal bisikletlerin olası durumda başta tükenmemesi için)
                if ((durak_nesnesi.normalBisiklet - 1 !=0) && (random1 < durak_nesnesi.normalBisiklet - 1 )) // eğer üretilen random sayı duraktaki normal bisiklet sayısından az ise tüm müşteriler normal bisiklet kiralar(listeye ilk eklenirken)
                {
                     duraga_eklenecek_musteri_say = random1;
                     Kiralanan_normal_bisiklet = random1;
                   
                }
                else if ((random1 > durak_nesnesi.normalBisiklet - 1)  &&  (random1 < kullanılabilecek_bisiklet))
                { // yeterli sayıda normal bisiklet yoksa ve random1 kullanılabilecek bisiklet sayısından küçükse kalan müşteri tandem bisiklet kiralar
                    duraga_eklenecek_musteri_say = random1;
                    Kiralanan_normal_bisiklet = durak_nesnesi.normalBisiklet - 1;
                    kiralanan_tandem_bisiklet = random1 - (durak_nesnesi.normalBisiklet - 1);
                }


                else if (random1 > kullanılabilecek_bisiklet)
                { // eğer rastgele üretilen sayı kullanılabilecek bisiklet asayısından büyükse kullanılabilecek bisiklet sayısı kadar müşteri durağa eklenir
                    duraga_eklenecek_musteri_say = kullanılabilecek_bisiklet;
                    Kiralanan_normal_bisiklet = durak_nesnesi.normalBisiklet - 1;
                    kiralanan_tandem_bisiklet = kullanılabilecek_bisiklet - (durak_nesnesi.normalBisiklet - 1);
                    
                }

                List<MusteriSınıfı> Duraktaki_musteri_listesi = Durak_musteri_listesi_Olustur(duraga_eklenecek_musteri_say); // durakta bulunan müşterilere ait listeyi oluşturan metot çağırıldı
                durak_nesnesi.durak_musterileri = Duraktaki_musteri_listesi; // oluşturulan müşteri listesi ilgili durak nesnesinin bellek sahasına atanıyor
                                                                             // BP ve bisiklet sayıları güncelleniyor
                durak_nesnesi.bosPark += Duraktaki_musteri_listesi.Count;  
                durak_nesnesi.normalBisiklet -= Kiralanan_normal_bisiklet; // müşteriler aldıktan sonra kalan normal bisiklet sayısı güncelleniyor
                durak_nesnesi.tandemBisiklet -= kiralanan_tandem_bisiklet;// müşteriler aldıktan sonra kalan tandem bisiklet sayısı güncelleniyor
                // oluşturulan durak nesnesi ve müşterilerin listesi ikili arama ağacına ekleniyor
                ikiliAramaAgaci.insert(durak_nesnesi); // müşterileri de içeren durak nesnesi ağaca ekleniyor
            }

            // oluşturulan ağaç inorder olarak dolaşılıyor ve bilgiler yazdırılıyor
            Console.WriteLine("******************İKİLİ ARAMA AĞACINA AİT BİLGİLER (müşteriler eklenmiş haliyle)*********************");
            Console.WriteLine("\nAgacın InOrder Dolasılması : ");
            ikiliAramaAgaci.inOrder(ikiliAramaAgaci.getRoot());
            Console.WriteLine();
            Console.WriteLine("***********LİSTE İÇERİĞİYLE BİRLİKTE DURAK BİLGİLERİ************");
            Console.WriteLine();
            Agac_derinligi_bul_bilgileri_yaz(ikiliAramaAgaci.getRoot(), -1);
            Console.WriteLine();
            Console.WriteLine("Ağacın derinliği :" + maxDepth);
            Console.WriteLine();
            Console.WriteLine(" müşterinin ID numarasını giriniz");
            int klavye_musteriID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Belirtilen_musterinin_kiralama_yaptığı_saatler(ikiliAramaAgaci.getRoot(), klavye_musteriID);
            Console.WriteLine();
            Console.WriteLine("Kiralama yapılacak durak adını giriniz");
            string kiralama_yapılacak_durak = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(" Kiralama yapacak olan müşterinin ID numarasını giriniz");
            int kiralama_yapan_musteriID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            // kiralama işlemi için oluşturulan metot çağırılıyor
            Belirtilen_Durak_ve_ID_icin_kiralama_islemi(ikiliAramaAgaci.getRoot(), kiralama_yapan_musteriID, kiralama_yapılacak_durak);
            
            Console.ReadKey();
        }


    }
}
