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
    public enum Status { B, S };

    public class Student : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
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
                if (dat == null && Student_izmena.active == 1)
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
        private string adr;
        public string Adr
        {
            get
            {
                if (adr == null && Student_izmena.active == 1)
                    return AdresaStanovanja.ToString();
                else
                    return adr;
            }
            set
            {
                if (value != adr)
                {
                    adr = value;
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
        private string mail;
        public string Mail
        {
            get => mail;
            set
            {
                if (value != mail)
                {
                    mail = value;
                    OnPropertyChanged();
                }
            }
        }
        private string brojIndeksa;
        public string BrojIndeksa
        {
            get => brojIndeksa;
            set
            {
                if (value != brojIndeksa)
                {
                    brojIndeksa = value;
                    OnPropertyChanged();
                }
            }
        }
        private int godinaUpisa;
        public int GodinaUpisa
        {
            get => godinaUpisa;
            set
            {
                if (value != godinaUpisa)
                {
                    godinaUpisa = Convert.ToInt32(GodUpisa);
                    OnPropertyChanged();
                }
            }
        }
        private string godUpisa;
        public string GodUpisa
        {
            get
            {
                if (godUpisa == null && Student_izmena.active == 1)
                    return GodinaUpisa.ToString();
                else
                    return godUpisa;
            }
            set
            {
                if (value != godUpisa)
                {
                    godUpisa = value;
                    godinaUpisa = Convert.ToInt32(godUpisa);
                    OnPropertyChanged();
                }
            }
        }
        private int trenutnaGodinaStudija;
        public int TrenutnaGodinaStudija
        {
            get => trenutnaGodinaStudija;
            set
            {
                if (value != trenutnaGodinaStudija)
                {
                    trenutnaGodinaStudija = value;
                    OnPropertyChanged();
                }
            }
        }
        private Status status;
        public Status Status
        {
            get { return status; }
            set { status = value; }
        }
        private double prosecnaOcena;
        public double ProsecnaOcena
        {
            get { return prosecnaOcena; }
            set { prosecnaOcena = value; }
        }
        public List<Ocena> spisakPolozenih { get; set; }
        public List<Predmet> spisakNepolozenih { get; set; }

        public Student(string brojIndeksa, string ime, string prezime, int trenutnaGodinaStudija, Status status, double prosecnaOcena)
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
            this.spisakPolozenih = new List<Ocena>();
        }

        public Student()
        {
            spisakPolozenih = new List<Ocena>();
        }

        public override string ToString()
        {
            return $"{BrojIndeksa}";
        }

        public string[] ToCSV()
        {
            string adresa = adresaStanovanja.id.ToString() + "," + adresaStanovanja.ulica + "," + adresaStanovanja.adresniBroj.ToString() + "," + adresaStanovanja.grad + "," + adresaStanovanja.drzava;
            string[] csvValues =
            {
                brojIndeksa,
                ime,
                prezime,
                datumRodjenja.ToString("MM.dd.yyyy"),
                adresa,
                kontaktTelefon,
                mail,
                godUpisa,
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
            //datumRodjenja = Convert.ToDateTime(values[3]);
            datumRodjenja = DateTime.Parse(values[3], new CultureInfo("en-US", false));
            string adresa = values[4];
            string[] deloviAdrese = adresa.Split(',');
            //int id = Convert.ToInt32(deloviAdrese[0]);
            string ulica = deloviAdrese[1];
            int adresniBroj = Convert.ToInt32(deloviAdrese[2]);
            string grad = deloviAdrese[3];
            string drzava = deloviAdrese[4];
            adresaStanovanja = new Adresa(ulica, adresniBroj, grad, drzava);
            kontaktTelefon = values[5];
            mail = values[6];
            godUpisa = values[7];
            trenutnaGodinaStudija = int.Parse(values[8]);
            if (values[9].Equals("B"))
                status = Status.B;
            else
                status = Status.S;
        }

        private Regex _IndexRegexIndex = new Regex("[A-Z]{2} [0-9]{1,3}-[0-9]{4}");
        private Regex _IndexRegexTelefon = new Regex("[0-9]{3}/[0-9]{3}-[0-9]{3}");
        private Regex _IndexRegexMail = new Regex(@"[a-z\.]+@mailinator.com");
        private Regex _IndexRegexGodina = new Regex("[0-9]{4}");
        private Regex _IndexRegexIme = new Regex("[A-Za-zčćžšđČĆŽŠĐ]+");
        private Regex _IndexRegexPrezime = new Regex("[A-Za-zčćžšđČĆŽŠĐ]+");
        private Regex _IndexRegexDatum = new Regex("[0-9]{2}.[0-9]{2}.[0-9]{4}.");
        private Regex _IndexRegexAdresa = new Regex(@"[A-Za-z\sčćžšđČĆŽŠĐ]+,[0-9]*,[A-Za-z\sčćžšđČĆŽŠĐ]+,[A-Za-z\sčćžšđČĆŽŠĐ]+");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "BrojIndeksa")
                {
                    if (string.IsNullOrEmpty(BrojIndeksa))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Indeks je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Index is necessary!";
                    }


                    Match match = _IndexRegexIndex.Match(BrojIndeksa);
                    if (!match.Success || !match.Value.Equals(BrojIndeksa))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Indeks treba biti u formatu: XY 123/YYYY";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Index should be in format: XY 123/YYYY";
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
                            return "Mora biti formata: MM.dd.YYYY";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Date should be in format: MM.dd.YYYY";
                    }
                }
                else if (columnName == "Adr")
                {
                    if (string.IsNullOrEmpty(Adr))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Adresa je neophodna!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Adress is necessary!";
                    }

                    Match match = _IndexRegexAdresa.Match(Adr);
                    if (!match.Success || !match.Value.Equals(Adr))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata: Ulica,broj,grad,drzava";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Adress should be in format: Street,number,city,state";
                    }
                }
                else if (columnName == "Ime")
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
                            return "Mora biti string!";
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
                            return "Mora biti string!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name should be string!";
                    }
                }
                else if (columnName == "Mail")
                {
                    if (string.IsNullOrEmpty(Mail))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "E-mail je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "E-mail is necessary!";
                    }

                    Match match = _IndexRegexMail.Match(Mail);
                    if (!match.Success || !match.Value.Equals(Mail))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti formata ...@mailinator.com";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "E-mail should be in format: ...@mailinator.com";
                    }
                }
                else if (columnName == "GodUpisa")
                {
                    if (string.IsNullOrEmpty(GodUpisa))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Godina je neophodna!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Year is necessary!";
                    }

                    Match match = _IndexRegexGodina.Match(GodUpisa);
                    if (!match.Success || !match.Value.Equals(GodUpisa))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti broj!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Year should be number!";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "BrojIndeksa", "KontaktTelefon", "Dat", "Adr", "Ime", "Prezime", "Mail", "GodUpisa" };

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
