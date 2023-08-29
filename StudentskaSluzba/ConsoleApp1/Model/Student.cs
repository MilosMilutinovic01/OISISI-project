using ConsoleApp1.Manager.Serialization;
using ConsoleApp1.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ConsoleApp1.Model
{
    public enum Status { B, S};

    public class Student : Serializable, INotifyPropertyChanged
    {
        private string ime;
        public string Ime
        {
            get { return ime; }
            set { ime = value; OnPropertyChanged("Ime"); }
        }
        private string prezime;
        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; OnPropertyChanged("Prezime"); }
        }
        public DateTime datumRodjenja { get; set; }
        public Adresa adresaStanovanja { get; set; }
        public string kontaktTelefon { get; set; }
        public string mail { get; set; }
        private string brojIndeksa;
        public string BrojIndeksa
        {
            get { return brojIndeksa; }
            set { brojIndeksa = value; OnPropertyChanged("Indeks"); }
        }
        private int godinaUpisa;
        public int GodinaUpisa
        {
            get { return godinaUpisa; }
            set { godinaUpisa = value; OnPropertyChanged("Godina"); }
        }
        public int trenutnaGodinaStudija { get; set; }
        private Status status;
        public Status Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged("Status"); }
        }
        private double prosecnaOcena;
        public double ProsecnaOcena
        {
            get { return prosecnaOcena; }
            set { prosecnaOcena = value; OnPropertyChanged("Prosek"); }
        }
        public List<Ocena> spisakPolozenih { get; set; }
        public List<NepolozeniPredmeti> spisakNepolozenih { get; set; }
        //AdresaManager managerAdresa;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Student(string brojIndeksa, string ime,string prezime,int trenutnaGodinaStudija, Status status,double prosecnaOcena)
        {
            this.brojIndeksa = brojIndeksa;
            this.ime = ime;
            this.prezime = prezime;
            this.trenutnaGodinaStudija = trenutnaGodinaStudija;
            this.status = status;
            this.prosecnaOcena = prosecnaOcena;
        }

        public Student(string ime, string prezime, DateTime datumRodjenja, Adresa adresaStanovanja, string kontaktTelefon, string mail, string brojIndeksa, int godinaUpisa, int trenutnaGodinaStudija, Status status, double prosecnaOcena)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
            this.adresaStanovanja = adresaStanovanja;
            this.kontaktTelefon = kontaktTelefon;
            this.mail = mail;
            this.brojIndeksa = brojIndeksa;
            this.godinaUpisa = godinaUpisa;
            this.trenutnaGodinaStudija = trenutnaGodinaStudija;
            this.status = status;
            this.prosecnaOcena = prosecnaOcena;
            //this.spisakPolozenih = spisakPolozenih;
            //this.spisakNepolozenih = spisakNepolozenih;
            this.spisakNepolozenih = new List<NepolozeniPredmeti>();
            this.spisakPolozenih = new List<Ocena>();
        }

        public Student()
        {
            spisakNepolozenih = new List<NepolozeniPredmeti>();
            spisakPolozenih = new List<Ocena>();
        }

        public override string ToString()
        {
            if(status == Status.B)
            {
                return "\t-Ime: " + ime + "\n\t-Prezime:  " + prezime + "\n\t-Datum rodjenja:  " + datumRodjenja.ToShortDateString() + "\n\t-Adresa stanovanja:  " + adresaStanovanja.ToString()
                + "\n\t-Kontakt telefon: " + kontaktTelefon + "\n\t-Email:  " + mail + "\n\t-Broj indeksa:  " + brojIndeksa
                + "\n\t-Godina upisa:  " + godinaUpisa.ToString() + "\n\t-Trenutna godina studija:  " + trenutnaGodinaStudija.ToString() + "\n\t-Status:  budzet\n\t-Prosecna ocena:  "
                + prosecnaOcena.ToString();
            }
            else
            {
                return "\t-Ime: " + ime + "\n\t-Prezime:  " + prezime + "\n\t-Datum rodjenja:  " + datumRodjenja.ToShortDateString() + "\n\t-Adresa stanovanja:  " + adresaStanovanja
                + "\n\t-Kontakt telefon: " + kontaktTelefon + "\n\t-Datum rodjenja:  " + mail + "\n\t-Broj indeksa:  " + brojIndeksa
                + "\n\t-Godina upisa:  " + godinaUpisa.ToString() + "\n\t-Trenutna godina studija:  " + trenutnaGodinaStudija.ToString() + "\n\t-Status:  samofinansiranje\n\t-Prosecna ocena:  "
                + prosecnaOcena.ToString();
            }
        }

        public string[] ToCSV()
        {
            string adresa = adresaStanovanja.id.ToString() + "," + adresaStanovanja.ulica + "," + adresaStanovanja.adresniBroj.ToString() + "," + adresaStanovanja.grad + "," + adresaStanovanja.drzava;
            string[] csvValues =
            {
                brojIndeksa,
                ime,
                prezime,
                datumRodjenja.ToShortDateString(),
                adresa,
                kontaktTelefon,
                mail,
                godinaUpisa.ToString(),
                trenutnaGodinaStudija.ToString(),
                status.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            brojIndeksa = values[0];
            ime = values[1];
            prezime = values[2];
            datumRodjenja = Convert.ToDateTime(values[3]);
            string adresa = values[4];
            string[] deloviAdrese = adresa.Split(',');
            //int id = Convert.ToInt32(deloviAdrese[0]);
            string ulica = deloviAdrese[1];
            int adresniBroj =Convert.ToInt32(deloviAdrese[2]);
            string grad = deloviAdrese[3];
            string drzava = deloviAdrese[4];
            adresaStanovanja = new Adresa(ulica, adresniBroj, grad, drzava);
            kontaktTelefon = values[5];
            mail = values[6];
            godinaUpisa = int.Parse(values[7]);
            trenutnaGodinaStudija = int.Parse(values[8]);
            if (values[9].Equals("B"))
                status = Status.B;
            else
                status = Status.S;
        }
    }
}
