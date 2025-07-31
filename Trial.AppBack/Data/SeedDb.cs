using Microsoft.EntityFrameworkCore;
using Trial.AppBack.LoadCountries;
using Trial.AppInfra;
using Trial.AppInfra.UserHelper;
using Trial.Domain.Entities;
using Trial.Domain.EntitiesGen;
using Trial.Domain.Enum;
using Trial.DomainLogic.TrialResponse;

namespace Trial.AppBack.Data;

public class SeedDb
{
    private readonly DataContext _context;
    private readonly IApiService _apiService;
    private readonly IUserHelper _userHelper;

    public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper)
    {
        _context = context;
        _apiService = apiService;
        _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckRolesAsync();
        await CheckSoftPlan();
        await CheckCountries();
        await CheckTherapeutics();
        await CheckEnrolling();
        await CheckIndication();
        await CheckSponsor();
        await CheckCro();
        await CheckIrb();
        await CheckUserAsync("Optimus U", "TrialPro", "optimusu.soft@gmail.com", "+1 786 503", UserType.Admin);
    }

    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        await _userHelper.CheckRoleAsync(UserType.Administrator.ToString());
        await _userHelper.CheckRoleAsync(UserType.Coordinator.ToString());
        await _userHelper.CheckRoleAsync(UserType.Investigator.ToString());
        await _userHelper.CheckRoleAsync(UserType.Researcher.ToString());
        await _userHelper.CheckRoleAsync(UserType.Monitor.ToString());
    }

    private async Task CheckIrb()
    {
        if (!_context.Irbs.Any())
        {
            var irbs = new List<Irb>
            {
                new Irb { Name = "Advarra", Active = true },
                new Irb { Name = "Alpha", Active = true },
                new Irb { Name = "IntegReview Ethical Review Board", Active = true },
                new Irb { Name = "New England IRB", Active = true },
                new Irb { Name = "Quorum IRB", Active = true },
                new Irb { Name = "RealTime-CTMS", Active = true },
                new Irb { Name = "Schulman and Associates", Active = true },
                new Irb { Name = "WCG IRB Conexus", Active = true },
                new Irb { Name = "Western Institutional Review Board", Active = true }
            };

            _context.Irbs.AddRange(irbs);
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckCro()
    {
        if (!_context.Cros.Any())
        {
            var cros = new List<Cro>
            {
                new Cro { Name = "Chiltern", Active = true },
                new Cro { Name = "Covance", Active = true },
                new Cro { Name = "Icon Clinical Research", Active = true },
                new Cro { Name = "INC Research", Active = true },
                new Cro { Name = "IQVIA", Active = true },
                new Cro { Name = "LabCorp", Active = true },
                new Cro { Name = "MedPace", Active = true },
                new Cro { Name = "N/A", Active = true },
                new Cro { Name = "Other", Active = true },
                new Cro { Name = "Parexel", Active = true },
                new Cro { Name = "PPD, Inc.", Active = true },
                new Cro { Name = "PRA International", Active = true },
                new Cro { Name = "Premier Research", Active = true },
                new Cro { Name = "Quintiles", Active = true },
                new Cro { Name = "RealTime-CTMS", Active = true },
                new Cro { Name = "Syneos", Active = true },
                new Cro { Name = "TKL Research", Active = true }
            };

            _context.Cros.AddRange(cros);
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckSponsor()
    {
        if (!_context.Sponsors.Any())
        {
            var sponsors = new List<Sponsor>
            {
                new Sponsor { Name = "Abbott Laboratories", Description = "", Active = true },
                new Sponsor { Name = "AbbVie Inc.", Description = "", Active = true },
                new Sponsor { Name = "Acadia Pharmaceuticals Inc.", Description = "", Active = true },
                new Sponsor { Name = "Acumen Pharmaceuticals, Inc.", Description = "", Active = true },
                new Sponsor { Name = "Aiolos Bio", Description = "", Active = true },
                new Sponsor { Name = "Akebia Therapeutics, Inc.", Description = "", Active = true },
                new Sponsor { Name = "Alnylam Pharmaceuticals, Inc.", Description = "", Active = true },
                new Sponsor { Name = "Amgen", Description = "", Active = true },
                new Sponsor { Name = "AriBio USA, Inc.", Description = "", Active = true },
                new Sponsor { Name = "AstraZeneca", Description = "", Active = true },
                new Sponsor { Name = "ATRI", Description = "", Active = true },
                new Sponsor { Name = "Axsome Therapeutics", Description = "", Active = true },
                new Sponsor { Name = "Bavarian Nordic", Description = "", Active = true },
                new Sponsor { Name = "Bio Vie, Inc.", Description = "", Active = true },
                new Sponsor { Name = "Boehringer Ingelheim", Description = "", Active = true },
                new Sponsor { Name = "Bristol Myers Squibb", Description = "", Active = true },
                new Sponsor { Name = "Cantor BioConnect", Description = "", Active = true },
                new Sponsor { Name = "Cara Therapeutics, Inc.", Description = "", Active = true },
                new Sponsor { Name = "CROMSOURCE", Description = "", Active = true },
                new Sponsor { Name = "Eisai Inc.", Description = "", Active = true },
                new Sponsor { Name = "Eli Lilly and Company", Description = "", Active = true },
                new Sponsor { Name = "Enanta Pharmaceuticals", Description = "", Active = true },
                new Sponsor { Name = "F. Hoffmann-La Roche Ltd", Description = "", Active = true },
                new Sponsor { Name = "Forest Research Institute", Description = "", Active = true },
                new Sponsor { Name = "Gilead", Description = "", Active = true },
                new Sponsor { Name = "GlaxoSmithKline", Description = "", Active = true },
                new Sponsor { Name = "Incyte Corporation", Description = "", Active = true },
                new Sponsor { Name = "INmune Bio", Description = "", Active = true },
                new Sponsor { Name = "Johnson & Johnson", Description = "", Active = true },
                new Sponsor { Name = "Life Molecular Imaging Ltd", Description = "", Active = true },
                new Sponsor { Name = "MedPace", Description = "", Active = true },
                new Sponsor { Name = "Merck", Description = "", Active = true },
                new Sponsor { Name = "NEURIM PHARMACEUTICALS (1991) LTD", Description = "", Active = true },
                new Sponsor { Name = "Novartis", Description = "", Active = true },
                new Sponsor { Name = "Novo Nordisk", Description = "", Active = true },
                new Sponsor { Name = "Pardes Biosciences", Description = "", Active = true },
                new Sponsor { Name = "Pfizer", Description = "", Active = true },
                new Sponsor { Name = "ProMIS Neurosciences", Description = "", Active = true },
                new Sponsor { Name = "RealTime-CTMS", Description = "", Active = true },
                new Sponsor { Name = "Sanofi Pasteur Inc.", Description = "", Active = true },
                new Sponsor { Name = "Terns Pharmaceuticals", Description = "", Active = true },
                new Sponsor { Name = "UC San Diego", Description = "", Active = true },
                new Sponsor { Name = "University of Arizona", Description = "", Active = true },
                new Sponsor { Name = "Ventus Therapeutics", Description = "", Active = true }
            };

            _context.Sponsors.AddRange(sponsors);
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckIndication()
    {
        if (!_context.Indications.Any())
        {
            var indications = new List<Indication>
            {
                new Indication { Name = "Acne Vulgaris", Description = "", Active = true },
                new Indication { Name = "Alzheimer's Disease", Description = "", Active = true },
                new Indication { Name = "Anemia", Description = "", Active = true },
                new Indication { Name = "Ankle Sprains", Description = "", Active = true },
                new Indication { Name = "Atopic Dermatitis", Description = "", Active = true },
                new Indication { Name = "Attention Deficit Hyperactivity Disorder (ADH)", Description = "", Active = true },
                new Indication { Name = "Back Pain", Description = "", Active = true },
                new Indication { Name = "Bacteria Vaginosis/Vulvovaginal Candidiasis", Description = "", Active = true },
                new Indication { Name = "Bacterial Conjuctivitis", Description = "", Active = true },
                new Indication { Name = "Bacterial Vaginosis (BV)", Description = "", Active = true },
                new Indication { Name = "Binge Eating Disorder (BED)", Description = "", Active = true },
                new Indication { Name = "Bipolar", Description = "", Active = true },
                new Indication { Name = "Bipolar I", Description = "", Active = true },
                new Indication { Name = "Breast Cancer", Description = "", Active = true },
                new Indication { Name = "Cardiovascular Risks", Description = "", Active = true },
                new Indication { Name = "Carotid and Vertebral Basilar Arterial Disease", Description = "", Active = true },
                new Indication { Name = "Celiac Disease", Description = "", Active = true },
                new Indication { Name = "Central Nervous System Lesions", Description = "", Active = true },
                new Indication { Name = "Chlamydia trachomatis Infection", Description = "", Active = true },
                new Indication { Name = "Chronic Kidney Disease", Description = "", Active = true },
                new Indication { Name = "Congestive Heart Failure", Description = "", Active = true },
                new Indication { Name = "Constipation", Description = "", Active = true },
                new Indication { Name = "Contraception", Description = "", Active = true },
                new Indication { Name = "Diabetes Type 1", Description = "", Active = true },
                new Indication { Name = "Diabetes Type 1 and 2", Description = "", Active = true },
                new Indication { Name = "Diabetes Type 2", Description = "", Active = true },
                new Indication { Name = "Diabetic Peripheral Neuropathy (DPN)", Description = "", Active = true },
                new Indication { Name = "Diverticulitis", Description = "", Active = true },
                new Indication { Name = "Dyslipidemia", Description = "", Active = true },
                new Indication { Name = "Dysmenorrhea", Description = "", Active = true },
                new Indication { Name = "Endometriosis", Description = "", Active = true },
                new Indication { Name = "Epilepsy", Description = "", Active = true },
                new Indication { Name = "Fibrocystic Breast Disease", Description = "", Active = true },
                new Indication { Name = "Foot Ulcer", Description = "", Active = true },
                new Indication { Name = "Gastric Ulcers", Description = "", Active = true },
                new Indication { Name = "Gastroesophageal Reflux Disease (GERD)", Description = "", Active = true },
                new Indication { Name = "General Anxiety Disorder", Description = "", Active = true },
                new Indication { Name = "Healthy Patients", Description = "", Active = true },
                new Indication { Name = "Healthy Subjects", Description = "", Active = true },
                new Indication { Name = "Hot Flashes", Description = "", Active = true },
                new Indication { Name = "HPV Infection", Description = "", Active = true },
                new Indication { Name = "Hypercholesterolemia", Description = "", Active = true },
                new Indication { Name = "Hypoactive Sexual Desire Disorder (HSDD)", Description = "", Active = true },
                new Indication { Name = "Hypoparathyroidism", Description = "", Active = true },
                new Indication { Name = "Implantable Device", Description = "", Active = true },
                new Indication { Name = "Influenza", Description = "", Active = true },
                new Indication { Name = "Intermittent Claudication (IC)", Description = "", Active = true },
                new Indication { Name = "Irritable Bowel Dysfunction (Constipation Predominate)", Description = "", Active = true },
                new Indication { Name = "Irritable Bowel Dysfunction (Diarrhea Predominate)", Description = "", Active = true },
                new Indication { Name = "Irritable Bowel Syndrome", Description = "", Active = true },
                new Indication { Name = "Liver Lesions", Description = "", Active = true },
                new Indication { Name = "Major Depressive Disorder", Description = "", Active = true },
                new Indication { Name = "MDD - Adjunct with SSRI", Description = "", Active = true },
                new Indication { Name = "MDD - Treatment Resistant", Description = "", Active = true },
                new Indication { Name = "Meniscectomy", Description = "", Active = true },
                new Indication { Name = "Migraine Headaches", Description = "", Active = true },
                new Indication { Name = "Mild Cognitive Impairment", Description = "", Active = true },
                new Indication { Name = "Myocardial Infarction", Description = "", Active = true },
                new Indication { Name = "Nasal Polyposis", Description = "", Active = true },
                new Indication { Name = "Non-cirrhotic Non-alcoholic Steatohepatitis", Description = "", Active = true },
                new Indication { Name = "Nonmalignant Pain", Description = "", Active = true },
                new Indication { Name = "NSAID Gastric Ulcer Prevention", Description = "", Active = true },
                new Indication { Name = "Obsessive Compulsive Disorder (OCD)", Description = "", Active = true },
                new Indication { Name = "Opioid Induced Bowel Dysfunction", Description = "", Active = true },
                new Indication { Name = "Opioid Induced Constipation", Description = "", Active = true },
                new Indication { Name = "Osteoarthritis", Description = "", Active = true },
                new Indication { Name = "Osteoporosis", Description = "", Active = true },
                new Indication { Name = "Otitis Externa", Description = "", Active = true },
                new Indication { Name = "Otitis Media", Description = "", Active = true },
                new Indication { Name = "Ovarian Activity", Description = "", Active = true },
                new Indication { Name = "Overactive Bladder", Description = "", Active = true },
                new Indication { Name = "Parkinson's Disease", Description = "", Active = true },
                new Indication { Name = "Perennial Allergic Rhinitis", Description = "", Active = true },
                new Indication { Name = "Peripheral Arterial Disease (PAD)", Description = "", Active = true },
                new Indication { Name = "Peripheral Neuropathic Pain", Description = "", Active = true },
                new Indication { Name = "Post-Microfracture Surgery", Description = "", Active = true },
                new Indication { Name = "Postherpetic Neuralgia (PHN)", Description = "", Active = true },
                new Indication { Name = "Prosthetic Joints", Description = "", Active = true },
                new Indication { Name = "Psoriasis", Description = "", Active = true },
                new Indication { Name = "Renal Arterial Disease", Description = "", Active = true },
                new Indication { Name = "Respiratory Syncytial Virus (RSV)", Description = "", Active = true },
                new Indication { Name = "Rosacea", Description = "", Active = true },
                new Indication { Name = "Schizophrenia", Description = "", Active = true },
                new Indication { Name = "Smoking Cessation", Description = "", Active = true },
                new Indication { Name = "Surgical Hemostasis", Description = "", Active = true },
                new Indication { Name = "Tendonitis", Description = "", Active = true },
                new Indication { Name = "Tourette's Disorder", Description = "", Active = true },
                new Indication { Name = "Ulcerative Colitis", Description = "", Active = true },
                new Indication { Name = "Urinary Tract Infection (UTI)", Description = "", Active = true },
                new Indication { Name = "Uterine Fibroids", Description = "", Active = true },
                new Indication { Name = "Vaccine", Description = "", Active = true },
                new Indication { Name = "Vaginal Atrophy", Description = "", Active = true },
                new Indication { Name = "Venous Leg Ulcers", Description = "", Active = true }
            };

            _context.Indications.AddRange(indications);
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckEnrolling()
    {
        if (!_context.Enrollings.Any())
        {
            var enrollings = new List<Enrolling>
            {
                new Enrolling { Name = "Enrolling - Web", Active = true },
                new Enrolling { Name = "Enrolling", Active = true },
                new Enrolling { Name = "Open", Active = true },
                new Enrolling { Name = "Upcomming", Active = true },
                new Enrolling { Name = "Study not Conducted", Active = true },
                new Enrolling { Name = "Inactive/Closed", Active = true },
                new Enrolling { Name = "Pending Study Closeout", Active = true },
            };

            _context.Enrollings.AddRange(enrollings);
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckTherapeutics()
    {
        if (!_context.TherapeuticAreas.Any())
        {
            var therapeuticAreas = new List<TherapeuticArea>
            {
                new TherapeuticArea { Name = "Adult Psychiatry", Description = "", Active = true },
                new TherapeuticArea { Name = "Cardiovascular", Description = "", Active = true },
                new TherapeuticArea { Name = "Child and Adolescent Psychiatry", Description = "", Active = true },
                new TherapeuticArea { Name = "Dermatology", Description = "", Active = true },
                new TherapeuticArea { Name = "Diabetes", Description = "", Active = true },
                new TherapeuticArea { Name = "Ear, Nose and Throat", Description = "", Active = true },
                new TherapeuticArea { Name = "Endocrinology", Description = "", Active = true },
                new TherapeuticArea { Name = "Future Studies", Description = "", Active = true },
                new TherapeuticArea { Name = "Gastroenterology", Description = "", Active = true },
                new TherapeuticArea { Name = "Musculoskeletal", Description = "", Active = true },
                new TherapeuticArea { Name = "Neurology", Description = "", Active = true },
                new TherapeuticArea { Name = "Ophthalmology", Description = "", Active = true },
                new TherapeuticArea { Name = "Pain Management / Anesthesiology", Description = "", Active = true },
                new TherapeuticArea { Name = "Pediatrics", Description = "", Active = true },
                new TherapeuticArea { Name = "Radiological Imaging", Description = "", Active = true },
                new TherapeuticArea { Name = "Rheumatology", Description = "", Active = true },
                new TherapeuticArea { Name = "Vascular Medicine/Surgery", Description = "", Active = true },
                new TherapeuticArea { Name = "Women's Health", Description = "", Active = true }
            };

            _context.TherapeuticAreas.AddRange(therapeuticAreas);
            await _context.SaveChangesAsync();
        }
    }

    private async Task<User> CheckUserAsync(string firstName, string lastName, string email,
        string phone, UserType userType)
    {
        User user = await _userHelper.GetUserAsync(email);
        if (user == null)
        {
            user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = $"{firstName} {lastName}",
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                JobPosition = "Administrador",
                UserFrom = "SeedDb",
                UserRoleDetails = new List<UserRoleDetails> { new UserRoleDetails { UserType = userType } },
                Active = true,
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());

            //Para Confirmar automaticamente el Usuario y activar la cuenta
            string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);
            await _userHelper.AddUserClaims(userType, email);
        }
        return user;
    }

    private async Task CheckSoftPlan()
    {
        if (!_context.SoftPlans.Any())
        {
            //Alimentando Planes
            _context.SoftPlans.Add(new SoftPlan
            {
                Name = "Plan 1 Mes",
                Price = 50,
                Meses = 1,
                TrialsCount = 2,
                Active = true
            });
            _context.SoftPlans.Add(new SoftPlan
            {
                Name = "Plan 6 Mes",
                Price = 300,
                Meses = 6,
                TrialsCount = 10,
                Active = true
            });
            _context.SoftPlans.Add(new SoftPlan
            {
                Name = "Plan 12 Mes",
                Price = 600,
                Meses = 12,
                TrialsCount = 100,
                Active = true
            });
            await _context.SaveChangesAsync();
        }
    }

    private async Task CheckCountries()
    {
        Response responseCountries = await _apiService.GetListAsync<CountryResponse>("/v1", "/countries");
        if (responseCountries.IsSuccess)
        {
            List<CountryResponse> NlistCountry = (List<CountryResponse>)responseCountries.Result!;
            List<CountryResponse> countries = NlistCountry.Where(x => x.Name == "Colombia" || x.Name == "United States").ToList();

            foreach (CountryResponse item in countries)
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == item.Name);
                if (country == null)
                {
                    country = new() { Name = item.Name!, States = new List<State>() };
                    Response responseStates = await _apiService.GetListAsync<StateResponse>("/v1", $"/countries/{item.Iso2}/states");
                    if (responseStates.IsSuccess)
                    {
                        List<StateResponse> states = (List<StateResponse>)responseStates.Result!;
                        foreach (StateResponse stateResponse in states!)
                        {
                            State state = country.States!.FirstOrDefault(s => s.Name == stateResponse.Name!)!;
                            if (state == null)
                            {
                                state = new() { Name = stateResponse.Name!, Cities = new List<City>() };
                                Response responseCities = await _apiService.GetListAsync<CityResponse>("/v1", $"/countries/{item.Iso2}/states/{stateResponse.Iso2}/cities");
                                if (responseCities.IsSuccess)
                                {
                                    List<CityResponse> cities = (List<CityResponse>)responseCities.Result!;
                                    foreach (CityResponse cityResponse in cities)
                                    {
                                        if (cityResponse.Name == "Mosfellsbær" || cityResponse.Name == "Șăulița")
                                        {
                                            continue;
                                        }
                                        City city = state.Cities!.FirstOrDefault(c => c.Name == cityResponse.Name!)!;
                                        if (city == null)
                                        {
                                            state.Cities.Add(new City() { Name = cityResponse.Name! });
                                        }
                                    }
                                }
                                if (state.CitiesNumber > 0)
                                {
                                    country.States.Add(state);
                                }
                            }
                        }
                    }
                    if (country.StatesNumber > 0)
                    {
                        _context.Countries.Add(country);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}