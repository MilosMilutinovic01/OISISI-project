using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Model
{
    public class Profesor : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ime;
        public string Ime
        {
            get { return ime; }
            set { ime = value; }
        }
        private string prezime;
        public string Prezime
        {
            get { return prezime; }
            set { prezime = value; }
        }
        private DateTime datumRodjenja;
        public DateTime DatumRodjenja
        {
            get => datumRodjenja;
            set
            {
                if (Convert.ToDateTime(Dat) != datumRodjenja)
                {
                    datumRodjenja = Convert.ToDateTime(Dat);
                    OnPropertyChanged();
                }
            }
        }
        private string dat;
        public string Dat
        {
            get
            {
                if (dat == null && Profesor_izmena.active == 1)
                    return DatumRodjenja.ToString("MM.dd.yyyy.");
                else
                    return dat;
            }
            set
            {
                if (value != dat)
                {
                    dat = value;
                    OnPropertyChanged();
                }
            }
        }
        private Adresa adresaStanovanja;
        public Adresa AdresaStanovanja
        {
            get => adresaStanovanja;
            set
            {
                if (value != adresaStanovanja)
                {
                    adresaStanovanja = value;
                    OnPropertyChanged();
                }
            }
        }
        private string adrsta;
        public string Adrsta
        {
            get
            {
                if (adrsta == null && Profesor_izmena.active == 1)
                    return adresaStanovanja.ToString();
                else
                    return adrsta;
            }
            set
            {
                if (value != adrsta)
                {
                    adrsta = value;
                    OnPropertyChanged();
                }
            }
        }
        private string kontaktTelefon;
        public string KontaktTelefon
        {
            get => kontaktTelefon;
            set
            {
                if (value != kontaktTelefon)
                {
                    kontaktTelefon = value;
                    OnPropertyChanged();
                }
            }
        }
        private string emailAdresa;
        public string EmailAdresa
        {
            get => emailAdresa;
            set
            {
                if (value != emailAdresa)
                {
                    emailAdresa = value;
                    OnPropertyChanged();
                }
            }
        }
        private Adresa adresaKancelarije;
        public Adresa AdresaKancelarije
        {
            get => adresaKancelarije;
            set
            {
                if (value != adresaKancelarije)
                {
                    adresaKancelarije = value;
                    OnPropertyChanged();
                }
            }
        }
        private string adrkan;
        public string Adrkan
        {
            get
            {
                if (adrkan == null && Profesor_izmena.active == 1)
                    return adresaKancelarije.ToString();
                else
                    return adrkan;
            }
            set
            {
                if (value != adrkan)
                {
                    adrkan = value;
                    OnPropertyChanged();
                }
            }
        }
        private string brojLicneKarte;
        public string BrojLicneKarte
        {
            get => brojLicneKarte;
            set
            {
                if (value != brojLicneKarte)
                {
                    brojLicneKarte = value;
                    OnPropertyChanged();
                }
            }
        }
        private string zvanje;
        public string Zvanje
        {
            get => zvanje;
            set
            {
                if (value != zvanje)
                {
                    zvanje = value;
                    OnPropertyChanged();
                }
            }
        }
        private int godineStaza;
        public int GodineStaza
        {
            get => godineStaza;
            set
            {
                if (value != godineStaza)
                {
                    godineStaza = Convert.ToInt32(GodStaza);
                    OnPropertyChanged();
                }
            }
        }
        private string godStaza;
        public string GodStaza
        {
            get
            {
                if (godStaza == null && Profesor_izmena.active == 1)
                    return GodineStaza.ToString();
                else
                    return godStaza;
            }
            set
            {
                if (value != godStaza)
                {
                    godStaza = value;
                    godineStaza = Convert.ToInt32(godStaza);
                    OnPropertyChanged();
                }
            }
        }
        private int idKatedre;
        public int IdKatedre//---------------------------------------odradi za katedru--------------------------!
        {
            get
            {
                return idKatedre;
            }
            set
            {
                idKatedre = value;
            }
        }
        public List<Predmet> predmeti { get; set; }

        public Profesor(string ime, string prezime, int godine)
        {
            Ime = ime;
            Prezime = prezime;
            GodineStaza = godine;
        }

        public Profesor(int id, string ime, string prezime, DateTime datumRodjenja, Adresa adresaStanovanja, string kontaktTelefon, string emailAdresa, Adresa adresaKancelarije, string brojLicneKarte, string zvanje, int godineStaza)    //, List<Predmet> listaPredmetaProfesora

        {
            this.id = id;
            this.ime = ime;
            this.prezime = prezime;
            this.datumRodjenja = datumRodjenja;
            this.adresaStanovanja = adresaStanovanja;
            this.kontaktTelefon = kontaktTelefon;
            this.EmailAdresa = emailAdresa;
            this.adresaKancelarije = adresaKancelarije;
            this.brojLicneKarte = brojLicneKarte;
            this.zvanje = zvanje;
            this.godineStaza = godineStaza;
            this.predmeti = new List<Predmet>();
        }

        public Profesor()
        {
            predmeti = new List<Predmet>();
        }

        public string[] ToCSV()
        {
            string adresas = adresaStanovanja.ulica + "," + adresaStanovanja.adresniBroj.ToString() + "," + adresaStanovanja.grad + "," + adresaStanovanja.drzava;
            string adresak = adresaKancelarije.ulica + "," + adresaKancelarije.adresniBroj.ToString() + "," + adresaKancelarije.grad + "," + adresaKancelarije.drzava;
            string[] csvValues =
            {
                id.ToString(),
                ime,
                prezime,
                datumRodjenja.ToShortDateString(),
                adresas,
                kontaktTelefon,
                EmailAdresa,
                adresak,
                brojLicneKarte,
                Zvanje,
                godineStaza.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = Convert.ToInt32(values[0]);
            ime = values[1];
            prezime = values[2];
            //datumRodjenja = Convert.ToDateTime(values[3]);
            datumRodjenja = DateTime.Parse(values[3], new CultureInfo("en-US", false));
            string adresas = values[4];
            string[] deloviAdrese = adresas.Split(',');
            string ulica = deloviAdrese[0];
            int adresniBroj = Convert.ToInt32(deloviAdrese[1]);
            string grad = deloviAdrese[2];
            string drzava = deloviAdrese[3];
            adresaStanovanja = new Adresa(id, ulica, adresniBroj, grad, drzava);
            kontaktTelefon = values[5];
            EmailAdresa = values[6];
            string adresak = values[7];
            string[] deloviAdresek = adresak.Split(',');
            string ulicak = deloviAdresek[0];
            int adresniBrojk = Convert.ToInt32(deloviAdresek[1]);
            string gradk = deloviAdresek[2];
            string drzavak = deloviAdresek[3];
            adresaKancelarije = new Adresa(id, ulicak, adresniBrojk, gradk, drzavak);
            brojLicneKarte = values[8];
            Zvanje = values[9];
            godineStaza = Convert.ToInt32(values[10]);
        }


        private Regex _IndexRegexIme = new Regex("[A-Za-zčćžšđČĆŽŠĐ]+");
        private Regex _IndexRegexPrezime = new Regex("[A-Za-zčćžšđČĆŽŠĐ]+");
        private Regex _IndexRegexZvanje = new Regex("[A-Za-z_]+");
        private Regex _IndexRegexMail = new Regex(@"[a-z\.]+@mailinator.com");
        private Regex _IndexRegexDatum = new Regex("[0-9]{2}.[0-9]{2}.[0-9]{4}.");
        private Regex _IndexRegexAdresa = new Regex(@"[A-Za-z\sčćžšđČĆŽŠĐ]+,[0-9]*,[A-Za-z\sčćžšđČĆŽŠĐ]+,[A-Za-z\sčćžšđČĆŽŠĐ]+");
        private Regex _IndexRegexTelefon = new Regex("[0-9]{3}/[0-9]{3}-[0-9]{3}");
        private Regex _IndexRegexLicna = new Regex("[0-9]{9}");
        private Regex _IndexRegexBroj = new Regex("[0-9]+");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Ime")
                {
                    if (string.IsNullOrEmpty(Ime))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Ime je neophodno!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name is necessary!";
                    }

                    Match match = _IndexRegexIme.Match(Ime);
                    if (!match.Success || !match.Value.Equals(Ime))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Ime mora biti string";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name should be string!";
                    }
                }
                else if (columnName == "Prezime")
                {
                    if (string.IsNullOrEmpty(Prezime))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Prezime je neophodno!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Last name is necessary!";
                    }

                    Match match = _IndexRegexPrezime.Match(Prezime);
                    if (!match.Success || !match.Value.Equals(Prezime))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Prezime mora biti stirng!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Last name should be string!";
                    }
                }
                else if (columnName == "Zvanje")
                {
                    if (string.IsNullOrEmpty(Zvanje))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Zvanje je neophodno!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Title is necessary!";
                    }

                    Match match = _IndexRegexZvanje.Match(Zvanje);
                    if (!match.Success || !match.Value.Equals(Zvanje))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Zvanje mora biti string!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Title should be string!";
                    }
                }
                else if (columnName == "EmailAdresa")
                {
                    if (string.IsNullOrEmpty(EmailAdresa))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "E-mail je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "E-mail is necessary!";
                    }

                    Match match = _IndexRegexMail.Match(EmailAdresa);
                    if (!match.Success || !match.Value.Equals(EmailAdresa))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata ...@mailinator.com";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "E-mail should be in format: ...@mailinator.com";
                    }
                }
                else if (columnName == "Dat")
                {
                    if (string.IsNullOrEmpty(Dat))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Datum rodjenja je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Birth date is necessary!";
                    }

                    Match match = _IndexRegexDatum.Match(Dat);
                    if (!match.Success || !match.Value.Equals(Dat))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata: MM/dd/YYYY";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Birth date should be in format: MM/dd/YYYY";
                    }
                }
                else if (columnName == "Adrsta")
                {
                    if (string.IsNullOrEmpty(Adrsta))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Adresa je neophodna!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Adress is necessary!";
                    }

                    Match match = _IndexRegexAdresa.Match(Adrsta);
                    if (!match.Success || !match.Value.Equals(Adrsta))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata: Ulica,broj,grad,drzava";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Adress should be in format: Street,number,city,state";
                    }
                }
                else if (columnName == "KontaktTelefon")
                {
                    if (string.IsNullOrEmpty(KontaktTelefon))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Broj je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Number is necessary!";
                    }

                    Match match = _IndexRegexTelefon.Match(KontaktTelefon);
                    if (!match.Success || !match.Value.Equals(KontaktTelefon))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata XXX/XXX-XXX!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Phone number should be in format XXX/XXX-XXX!";
                    }
                }
                else if (columnName == "Adrkan")
                {
                    if (string.IsNullOrEmpty(Adrkan))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Adresa je neophodna!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Adress is necessary!";
                    }

                    Match match = _IndexRegexAdresa.Match(Adrkan);
                    if (!match.Success || !match.Value.Equals(Adrkan))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata: Ulica,broj,grad,drzava";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Adress should be in format: Street,number,city,state";
                    }
                }
                else if (columnName == "BrojLicneKarte")
                {
                    if (string.IsNullOrEmpty(BrojLicneKarte))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Broj licne karte je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "IdCard number is necessary!";
                    }

                    Match match = _IndexRegexLicna.Match(BrojLicneKarte);
                    if (!match.Success || !match.Value.Equals(BrojLicneKarte))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti broj!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "IdCard number should be number!";
                    }
                }
                else if (columnName == "GodStaza")
                {
                    if (string.IsNullOrEmpty(GodStaza))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Godine staza su neophodne!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Experience is necessary!";
                    }

                    Match match = _IndexRegexBroj.Match(GodStaza);
                    if (!match.Success || !match.Value.Equals(GodStaza))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti broj!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Experience should be number!";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Ime", "Prezime", "Dat", "Zvanje", "EmailAdresa", "Adrsta", "KontaktTelefon", "Adrkan", "BrojLicneKarte", "GodStaza" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
