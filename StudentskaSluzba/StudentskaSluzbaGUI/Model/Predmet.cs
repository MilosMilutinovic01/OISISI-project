using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Model
{
    public enum Semestar { L, Z };

    public class Predmet : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        private string sifraPredmeta;
        public string SifraPredmeta
        {
            get => sifraPredmeta;
            set
            {
                if (value != sifraPredmeta)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged();
                }
            }
        }
        private string nazivPredmeta;
        public string NazivPredmeta
        {
            get => nazivPredmeta;
            set
            {
                if (value != nazivPredmeta)
                {
                    nazivPredmeta = value;
                    OnPropertyChanged();
                }
            }
        }
        private Semestar semestar;
        public Semestar Semestar
        {
            get { return semestar; }
            set { semestar = value; }
        }
        private int godinaStudija;
        public int GodinaStudija
        {
            get => godinaStudija;
            set
            {
                Convert.ToInt32(GodStudija);
            }
        }
        private string godStudija;
        public string GodStudija
        {
            get
            {
                if (godStudija == null && Predmet_izmena.active == 1)
                    return GodinaStudija.ToString();
                else
                    return godStudija;
            }
            set
            {
                if (value != godStudija)
                {
                    godStudija = value;
                    godinaStudija = Convert.ToInt32(godStudija);
                    OnPropertyChanged();
                }
            }
        }
        private int brojESPB;
        public int BrojESPB
        {
            get => brojESPB;
            set
            {
                if (value != brojESPB)
                {
                    brojESPB = value;
                    OnPropertyChanged();
                }
            }
        }
        private string brESPB;
        public string BrESPB
        {
            get
            {
                if (brESPB == null && Predmet_izmena.active == 1)
                    return BrojESPB.ToString();
                else
                    return brESPB;
            }
            set
            {
                if (value != brESPB)
                {
                    brESPB = value;
                    brojESPB = Convert.ToInt32(brESPB);
                    OnPropertyChanged();
                }
            }
        }
        public List<Student> polozili { get; set; }
        public List<Student> nisuPolozili { get; set; }
        private int profesorId; //moramo imati id profesora
        public int ProfesorId
        {
            get => profesorId;
            set
            {
                if (value != profesorId)
                {
                    profesorId = value;
                    OnPropertyChanged();
                }
            }
        }
        private string profId;
        public string ProfId
        {
            get
            {
                if (profId == null && Predmet_izmena.active == 1)
                    return ProfesorId.ToString();
                else
                    return profId;
            }
            set
            {
                if (value != profId)
                {
                    profId = value;
                    profesorId = Convert.ToInt32(profId);
                    OnPropertyChanged();
                }
            }
        }
        public Profesor Profesor { get; set; }

        public Predmet(string sifraPredmeta, string nazivPredmeta, Semestar semestar, int godinaStudija, string predmetniProfesor, int brojESPB, int profesorId)
        {
            this.sifraPredmeta = sifraPredmeta;
            this.nazivPredmeta = nazivPredmeta;
            this.semestar = semestar;
            this.godinaStudija = godinaStudija;
            //this.predmetniProfesor = predmetniProfesor;
            this.brojESPB = brojESPB;
            this.polozili = polozili;
            this.nisuPolozili = nisuPolozili;
            this.ProfesorId = profesorId;
        }

        public Predmet()
        {

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                sifraPredmeta,
                nazivPredmeta,
                semestar.ToString(),
                godinaStudija.ToString(),
                //predmetniProfesor,
                brojESPB.ToString(),
                ProfesorId.ToString()//profesor id
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            sifraPredmeta = values[0];
            nazivPredmeta = values[1];
            if (values[2].Equals("L"))
                semestar = Semestar.L;
            else
                semestar = Semestar.Z;
            godinaStudija = int.Parse(values[3]);
            //predmetniProfesor = values[4];
            brojESPB = int.Parse(values[4]);
            ProfesorId = Convert.ToInt32(values[5]);//profesor id
        }


        private Regex _IndexRegexSifra = new Regex("[a-z]{1}[1-9]{1}");
        private Regex _IndexRegexNaziv = new Regex(@"[A-Za-zčČćĆšŠđĐžŽ\s]+");
        private Regex _IndexRegexGodina = new Regex("[1-5]{1}");
        private Regex _IndexRegexESPB = new Regex("[1-9]{1}");
        private Regex _IndexRegexProfesor = new Regex("[0-9]+");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "SifraPredmeta")
                {
                    if (string.IsNullOrEmpty(SifraPredmeta))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Šifra je neophodna!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Code is necessary!";
                    }

                    Match match = _IndexRegexSifra.Match(SifraPredmeta);
                    if (!match.Success || !match.Value.Equals(SifraPredmeta))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Šifra je formata: xa(x je karakter a je broj)";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Code should be in format: xa(x is character, a is number)";
                    }
                }
                else if (columnName == "NazivPredmeta")
                {
                    if (string.IsNullOrEmpty(NazivPredmeta))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Naziv je neophodan!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name is necessary!";
                    }

                    Match match = _IndexRegexNaziv.Match(NazivPredmeta);
                    if (!match.Success || !match.Value.Equals(NazivPredmeta))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Naziv je string!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name should be string!";
                    }
                }
                else if (columnName == "GodStudija")
                {
                    if (string.IsNullOrEmpty(GodStudija))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Godina studija je neophodna!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Year of studying is necessary!";
                    }

                    Match match = _IndexRegexGodina.Match(GodStudija);
                    if (!match.Success || !match.Value.Equals(GodStudija))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti broj!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name should be number!";
                    }
                }
                else if (columnName == "BrESPB")
                {
                    if (string.IsNullOrEmpty(BrESPB))
                        return "Broj ESPB je neophodan";

                    Match match = _IndexRegexESPB.Match(BrESPB);
                    if (!match.Success || !match.Value.Equals(BrESPB))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti broj!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name should be number!";
                    }
                }
                else if (columnName == "ProfId")
                {
                    if (string.IsNullOrEmpty(ProfId))
                        return "Id profesora je neophodan";

                    Match match = _IndexRegexESPB.Match(ProfId);
                    if (!match.Success || !match.Value.Equals(ProfId))
                    {
                        if (MainWindow.lang.Equals("sr-Latn-RS"))
                            return "Mora biti broj!";
                        else if (MainWindow.lang.Equals("en-US"))
                            return "Name should be number!";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "SifraPredmeta", "NazivPredmeta", "GodStudija", "BrESPB", "ProfId" };

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
