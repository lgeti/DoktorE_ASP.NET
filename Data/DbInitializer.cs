using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Data
{
    public static class DbInitializer
    {
        public static async void Initialize(DoktorEContext context)
        {
            context.Database.EnsureCreated();

            // Look for any clinics.
            if (context.Clinics.Any())
            {
                return;   // DB has been seeded
            }

            var clinics = new Clinic[]
            {
                new Clinic{typeOfClinic="Primary care"},
                new Clinic{typeOfClinic="Dental"},
                new Clinic{typeOfClinic="Dermatology"},
            };
            foreach (Clinic c in clinics)
            {
                context.Clinics.Add(c);
            }
            context.SaveChanges();

            // Look for any doctors.
            if (context.Doctors.Any())
            {
                return;   // DB has been seeded
            }

            var doctors = new Doctor[]
            {
                new Doctor{Ime="Mojca", Priimek="Manova", Specialty="Primary care", ClinicID=1},
                new Doctor{Ime="Mitja", Priimek="Jaki", Specialty="Dental", ClinicID=2},
                new Doctor{Ime="Benca", Priimek="Dobra", Specialty="Dermatology", ClinicID=3},
            };
            foreach (Doctor d in doctors)
            {
                context.Doctors.Add(d);
            }
            context.SaveChanges();

            // Look for any patients.
            if (context.Patients.Any())
            {
                return;   // DB has been seeded
            }

            var patients = new Patient[]
            {
                new Patient{ZZZS=1234567890, Ime="Alice", Priimek="Smith"},
                new Patient{ZZZS=2345678901, Ime="Bob", Priimek="Drew"},
                new Patient{ZZZS=3456789012, Ime="Charlie", Priimek="Stan"},
            };
            foreach (Patient p in patients)
            {
                context.Patients.Add(p);
            }
            context.SaveChanges();

            // Look for any blood donations.
            if (context.BloodDonations.Any())
            {
                return;   // DB has been seeded
            }

            var bloodDonations = new BloodDonation[]
            {
                new BloodDonation{date=DateTime.Parse("2022-02-11"), PatientID=1},
                new BloodDonation{date=DateTime.Parse("2022-02-12"), PatientID=2},
                new BloodDonation{date=DateTime.Parse("2022-02-13"), PatientID=3},
            };
            foreach (BloodDonation bd in bloodDonations)
            {
                context.BloodDonations.Add(bd);
            }
            context.SaveChanges();

            var appointments = new Appointment[]
            {
                new Appointment{AppointmentDate=DateTime.Parse("2022-12-24"), PatientID=1, DoctorID=1, DoctorsNote="1"},
                new Appointment{AppointmentDate=DateTime.Parse("2022-12-25"), PatientID=2, DoctorID=2, DoctorsNote="2"},
                new Appointment{AppointmentDate=DateTime.Parse("2022-12-26"), PatientID=3, DoctorID=3, DoctorsNote="3"},
            };
            foreach (Appointment ap in appointments)
            {
                context.Appointments.Add(ap);
            }
            context.SaveChanges();

            var invoices = new Invoice[]
            {
                new Invoice{sum=350, PatientID=1, AppointmentID=1, AppointmentDate=DateTime.Parse("2022-12-24")},
                new Invoice{sum=3200, PatientID=2, AppointmentID=2, AppointmentDate=DateTime.Parse("2022-12-25")},
                new Invoice{sum=888, PatientID=3, AppointmentID=3, AppointmentDate=DateTime.Parse("2022-12-26")},
            };
            foreach (Invoice inv in invoices)
            {
                context.Invoices.Add(inv);
            }
            context.SaveChanges();

             var prescr = new Prescription[]
            {
                new Prescription{drugs="Paracetamol i zabava", PatientID=1,expiryDate=DateTime.Parse("2023-12-24")},
                new Prescription{drugs="Cist vozduh i fizicka aktivnost", PatientID=2,expiryDate=DateTime.Parse("2023-12-25")},
                new Prescription{drugs="voda ama ne mnogu i malce sok", PatientID=3,expiryDate=DateTime.Parse("2023-12-26")},
            };
            foreach (Prescription pr in prescr)
            {
                context.Prescriptions.Add(pr);
            }
            context.SaveChanges();

            var roles = new IdentityRole[] {
                new IdentityRole{Id="1", Name="Administrator"},
                new IdentityRole{Id="2", Name="Manager"},
                new IdentityRole{Id="3", Name="Staff"}
            };

            foreach (IdentityRole r in roles)
            {
                context.Roles.Add(r);
            }

            var user = new ApplicationUser
            {
                FirstName = "Bob",
                LastName = "Dilon",
                City = "Ljubljana",
                Email = "bob@example.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "bob@example.com",
                NormalizedUserName = "bob@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Testni123!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
            }

            context.SaveChanges();

            var doctor = new ApplicationUser
            {
                FirstName = "Pero",
                LastName = "Trapero",
                City = "Skopje",
                Email = "pero@trapero.com",
                NormalizedEmail = "PERO@TRAPERO.COM",
                UserName = "pero@trapero.com",
                NormalizedUserName = "pero@trapero.com",
                PhoneNumber = "+38970345555",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == doctor.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(doctor,"Testni123!");
                doctor.PasswordHash = hashed;
                context.Users.Add(doctor);
            }

            context.SaveChanges();

            var pacient = new ApplicationUser
            {
                FirstName = "Mojca",
                LastName = "Mojcovska",
                City = "Ljubljana",
                Email = "mojca@gmail.com",
                NormalizedEmail = "MOJCA@GMAIL.COM",
                UserName = "mojca@gmail.com",
                NormalizedUserName = "mojca@gmail.com",
                PhoneNumber = "+38670373303",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == pacient.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(pacient,"Testni123!");
                pacient.PasswordHash = hashed;
                context.Users.Add(pacient);
            }

            var ignat = new ApplicationUser
            {
                FirstName = "Ignat",
                LastName = "Pocevski",
                City = "Kriva Palanka",
                Email = "ignatpocevski@gmail.com",
                NormalizedEmail = "ignatpocevski@GMAIL.COM",
                UserName = "ignatpocevski@gmail.com",
                NormalizedUserName = "ignatpocevski@gmail.com",
                PhoneNumber = "+38669737050",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == ignat.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(ignat,"Testni123!");
                ignat.PasswordHash = hashed;
                context.Users.Add(ignat);
            }
            context.SaveChanges();

            
            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId = user.Id},
                new IdentityUserRole<string>{RoleId = roles[1].Id, UserId = user.Id},
                new IdentityUserRole<string>{RoleId = roles[2].Id, UserId = doctor.Id},
                new IdentityUserRole<string>{RoleId = roles[0].Id, UserId = ignat.Id}
            };

            foreach (IdentityUserRole<string> r in UserRoles)
            {
                context.UserRoles.Add(r);
            }

            context.SaveChanges();
        }
    }
}