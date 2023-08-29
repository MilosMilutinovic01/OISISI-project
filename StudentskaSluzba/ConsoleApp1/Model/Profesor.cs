using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ConsoleApp1.Model
{
    public class Profesor : Serializable, INotifyPropertyChanged
    {
        public int id { get; set; }
        public string prezime;
        public string ime;
        public DateTime datumRodjenja { get; set; }
        public Adresa adresaStanovanja { get; set; }
        public string kontaktTelefon { get; set; }
        public string emailAdresa { get; set; }
        public Adresa adresaKancelarije { get; set; }
        public string brojLicneKarte { get; set; }
        public string zvanje { get; set; }
        public int godineStaza;
        public List<Predmet> predmeti { get; set; }//lista predmeta

        public string idKat { get; set; }

        public string Ime
        {
            get { return ime; }
            set { ime = value; OnPropertyChanged("Ime"); }
        }
        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; OnPropertyChanged("Prezime"); }
        }
        public int GodineStaza
        {
            get { return godineStaza; }
            set { godineStaza = value; OnPropertyChanged("Godine"); }
        }

        public Profesor(string ime, string prezime, int godine)
        {
            Ime = ime;
            Prezime = prezime;
            GodineStaza = godine;
        }

        public Profesor(int id, string ime, string prezime, DateTime datumRodjenja, Adresa adresaStanovanja, string kontaktTelefon, string emailAdresa, Adresa adresaKancelarije, string brojLicneKarte, string zvanje, int godineStaza, List<Predmet> listaPredmetaProfesora, string idKat)    //, List<Predmet> listaPredmetaProfesora

        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
            this.adresaStanovanja = adresaStanovanja;
            this.kontaktTelefon = kontaktTelefon;
            this.emailAdresa = emailAdresa;
            this.adresaKancelarije = adresaKancelarije;
            this.brojLicneKarte = brojLicneKarte;
            this.zvanje = zvanje;
            this.godineStaza = godineStaza;
            this.predmeti = new List<Predmet>();
            this.idKat = idKat;
        }

        public Profesor()
        {
            predmeti = new List<Predmet>();
            idKat = "none";
        }


        public override string ToString()
        {

            if (predmeti.Any())
            {


                string listapredmeta = "\n\tLista predmeta: \n";
                foreach (Predmet predmet in predmeti)
                {
                    listapredmeta += predmet.ToString();
                }

                return string.Format("\t-ID: " + Convert.ToString(id) + "\n\t-Ime: " + ime + "\n\t-Prezime: " + prezime +
                "\n\t-Datum Rodjenja: " + datumRodjenja.ToShortDateString() + "\n\t-Adresa stanovanja: " + adresaStanovanja.ToString() +
                "\n\t-Kontakt Telefon: " + kontaktTelefon + "\n\t-Email Adresa: " + emailAdresa +
                 "\n\t-Adresa kancelarije: " + adresaKancelarije.ToString() + "\n\t-Broj Licne Karte: " + brojLicneKarte +
                "\n\t-Zvanje: " + zvanje + "\n\t-Godine Staza: " + Convert.ToString(godineStaza)) + listapredmeta;
            }
            else
            {
                return string.Format("\t-ID: " + Convert.ToString(id) + "\n\t-Ime: " + ime + "\n\t-Prezime: " + prezime +
                "\n\t-Datum Rodjenja: " + datumRodjenja.ToShortDateString() + "\n\t-Adresa stanovanja: " + adresaStanovanja.ToString() +
                "\n\t-Kontakt Telefon: " + kontaktTelefon + "\n\t-Email Adresa: " + emailAdresa +
                 "\n\t-Adresa kancelarije: " + adresaKancelarije.ToString() + "\n\t-Broj Licne Karte: " + brojLicneKarte +
                "\n\t-Zvanje: " + zvanje + "\n\t-Godine Staza: " + Convert.ToString(godineStaza));


            }
        }
        
        public string[] ToCSV()
        {
            string adresas = adresaStanovanja.ulica + " " + adresaStanovanja.adresniBroj.ToString() + " " + adresaStanovanja.grad + " " + adresaStanovanja.drzava;
            string adresak = adresaKancelarije.ulica + " " + adresaKancelarije.adresniBroj.ToString() + " " + adresaKancelarije.grad + " " + adresaKancelarije.drzava;
            string[] csvValues =
            {
                id.ToString(),
                ime,
                prezime,
                datumRodjenja.ToShortDateString(),
                adresas,
                kontaktTelefon,
                emailAdresa,
                adresak,
                brojLicneKarte,
                zvanje,
                godineStaza.ToString(),
                idKat.ToString()
            };
            return csvValues;
        }
        
        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            ime = values[1];
            prezime = values[2];
            datumRodjenja = Convert.ToDateTime(values[3]);
            string adresas = values[4];
            string[] deloviAdrese = adresas.Split(' ');
            string ulica = deloviAdrese[0];
            int adresniBroj = Convert.ToInt32(deloviAdrese[1]);
            string grad = deloviAdrese[2];
            string drzava = deloviAdrese[3];
            adresaStanovanja = new Adresa(id,ulica, adresniBroj, grad, drzava);
            kontaktTelefon = values[5];
            emailAdresa = values[6];
            string adresak = values[7];
            string[] deloviAdresek = adresak.Split(' ');
            string ulicak = deloviAdresek[0];
            int adresniBrojk = Convert.ToInt32(deloviAdresek[1]);
            string gradk = deloviAdresek[2];
            string drzavak = deloviAdresek[3];
            adresaKancelarije = new Adresa(id,ulicak, adresniBrojk, gradk, drzavak);
            brojLicneKarte = values[8];
            zvanje = values[9];
            godineStaza = int.Parse(values[10]);
            idKat = values[11];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
