namespace AppTheatre.Migrations
{
    using AppTheatre.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppTheatre.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppTheatre.Models.ApplicationDbContext context)
        {
            context.Vrste.AddOrUpdate(x => x.Id,

                new VrstaPredstave() { Id = 1, Naziv = "Tragedija", Opis= "Tragedija je dramska vrsta u stihovima koja se razvila u staroj Grckoj, postignuvsi najveci procvat u V veku pne." },
                new VrstaPredstave() { Id = 2, Naziv = "Komedija", Opis = "Komedija je vrsta drame,odlikuju je duhoviti detalji, crta smesne strane ljudskog zivota i ljudi, ismejava njihove nedostatke i mane. "},
                new VrstaPredstave() { Id = 3, Naziv = "Opera" , Opis = "Opera se može definisati kao scenska drama u kojoj glavni glumci pevaju vecinu svoje uloge. Smatra se jednim od najslozenijih oblika umetnosti. " },
                new VrstaPredstave() { Id = 4, Naziv = "Balet", Opis = "Balet je plesna forma koju danas najcesce vidimo u izvodjenju profesionalnih plesaca u pozoristu ili na nekoj drugoj sceni."},
                new VrstaPredstave() { Id = 5, Naziv = "Mjuzikl", Opis = "Mjuzikl je muzicko – scensko delo zabavnog karaktera s govorenim dijalozima, muzickim i plesnim tackama, najcesce u dva cina."},
                new VrstaPredstave() { Id = 6, Naziv = "Lutkarska", Opis = "Lutkarstvo je grana teatra u kojoj predstavu izvode lutke, koje na sceni pokrecu glumci - lutkari, najčešće skriveni od gledaoca, oni umesto lutaka izgovaraju tekst uloge koju glumi lutka." },
                new VrstaPredstave() { Id = 7, Naziv = "Drama", Opis= "Prevedeno s grckog jezika, drama znaci radnja." +
                " Ona obuhvata sve knjizevne vrste namenjene izvodjenju na pozornici," +
                " a koje svoj pravi smisao dobivaju u pozorisnoj predstavi." }
                
                );

            context.Pozorista.AddOrUpdate(x => x.Id,
                new Pozoriste() { Id = 1, Naziv = "Akademija 28", UpravnikImePrezime="Mara Maric", GodinaOsnivanja = 1991, Grad = "Beograd", Adresa = "Nemanjina 28", Telefon = "011 - 3621 - 787", Email = "akademija28@yahoo.com" },
                new Pozoriste() { Id = 2, Naziv = "Atelje 212",  UpravnikImePrezime = "Steva Stevic", GodinaOsnivanja = 1956, Grad = "Beograd", Adresa = "Svetogorska 21", Telefon = "011 - 3247 - 342", Email = "atelje212bilet@atelje212.rs" },
                new Pozoriste() { Id = 3, Naziv = "Srpsko narodno pozoriste",  UpravnikImePrezime = "Mika Pantic", GodinaOsnivanja = 1861, Grad = "Novi Sad", Adresa = "Pozorisni trg 1", Telefon = "021 - 6621 - 411", Email = "biletarnica@snp.org.rs" },
                new Pozoriste() { Id = 4, Naziv = "Bitef teatar", UpravnikImePrezime = "Stevica Stevic", GodinaOsnivanja = 1967, Grad = "Beograd", Adresa = "Skver Mire Trailović 1", Telefon = "011 - 3243 - 108", Email = "blagajna@bitef.rs" },
                new Pozoriste() { Id = 5, Naziv = "Narodno Pozoriste Sombor", UpravnikImePrezime = "Rados Adic", GodinaOsnivanja = 1882, Grad = "Sombor", Adresa = "Trg Koste Trifkovica 2", Telefon = "025 - 4363 - 173", Email = "sombornationaltheatre@gmail.com" },
                new Pozoriste() { Id = 6, Naziv = "Pozoriste Mladih", UpravnikImePrezime = "Ana Anic", GodinaOsnivanja = 1931, Grad = "Novi Sad", Adresa = "Ignjata Pavlasa 4", Telefon = "021 - 6623 - 210", Email = "office@pozoristemladih.co.rs" },
                new Pozoriste() { Id = 7, Naziv = "Jugoslovensko dramsko pozoriste",  UpravnikImePrezime = "Marko Markic", GodinaOsnivanja = 1947, Grad = "Beograd", Adresa = "Kralja Milana 50", Telefon ="011 - 3061 - 900", Email = "jdppr@jdp.rs" }

                );

            context.Glumci.AddOrUpdate(x => x.Id,
                new Glumac() { Id = 1, Ime = "Ivana", Prezime= "Knezevic", GodinaRodjenja= 1986 },
                new Glumac() { Id = 2, Ime = "Miroljub", Prezime = "Turajlija", GodinaRodjenja = 1980},
                new Glumac() { Id = 3, Ime = "Nebojsa", Prezime = "Dugajlic", GodinaRodjenja = 1970},
                new Glumac() { Id = 4, Ime = "Ljiljana", Prezime = "Blagojevic", GodinaRodjenja = 1955},
                new Glumac() { Id = 5, Ime = "Aleksandra", Prezime = "Nikolic", GodinaRodjenja = 1954},
                new Glumac() { Id = 6, Ime = "Danica", Prezime = "Ristovski", GodinaRodjenja = 1974},
                new Glumac() { Id = 7, Ime = "Milorad", Prezime = "Damjanovic", GodinaRodjenja = 1988}
                );

            context.Predstave.AddOrUpdate(x => x.Id,

              new Predstava()
              {
                  Id = 1,
                  Naziv = "Pijani",
                  ImeRezisera = "Boris Lijesevic",
                  Opis = "Pijani u sebi sadrze opomenu i poziv svima nama da se vise ne bojimo i da se konacno prepustimo zivotu," +
                  "da prestanemo da se plasimo da volimo, da oprastamo, da menjamo sebe i svet oko sebe. Pijani su filozofska komedija" +
                  " o samospoznaji i otkrivanju. ",
                  DatumOdrazavanja = DateTime.Parse("2019-05-25"),
                  VremeOdrzavanja = DateTime.Parse("20:30:00"),
                  CenaKarte = 1500,
                  Premijera = DateTime.Parse("2016-02-16"),
                  VrstaPredstaveId = 2,
                  PozoristeId = 2
              }
              );



            context.Veze.AddOrUpdate(x => x.Id,

            new Veza() { Id = 1, GlumacId = 6, PredstavaId = 1 });


            context.SaveChanges();
        }
    }
}
