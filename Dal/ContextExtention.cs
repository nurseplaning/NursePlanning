using DomainModel;
using System;
using System.Linq;

namespace Dal
{
	public static class ContextExtention
	{
		public static void Initialize(this ApplicationDbContext context, bool dropAlways = false)
		{
			if (dropAlways)
				context.Database.EnsureDeleted();
			try
			{
				context.Database.EnsureCreated();

			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.ToString());
			}

			if (context.Statuses.Any())
				return;

			var status = new Status[] {
				new Status { Name = "En attente" },
				new Status { Name = "Validé" },
				new Status { Name = "Annulé" },
				new Status { Name = "Rejeté" },
				new Status { Name = "Fermé" } };

			context.Statuses.AddRange(status);
			context.SaveChanges();

			var healthCarePrimaries = new HealthCarePrimary[] {
				new HealthCarePrimary { Name = "Soins d'hygiène" },// HealthCareSecondaries = new HealthCareSecondary[] { new HealthCareSecondary{ Name = "Aide à la toilette" } } },
                new HealthCarePrimary { Name = "Injections intraveineuses" },
				new HealthCarePrimary { Name = "Prélèvements sanguins, urinaires, bactériologiques, cytologiques" },
				new HealthCarePrimary { Name = "Surveillance des paramètres vitaux" },
				new HealthCarePrimary { Name = "Administration des traitements" },
				new HealthCarePrimary { Name = "Pansements" },
				new HealthCarePrimary { Name = "Soins sur chambre implantable" },
				new HealthCarePrimary { Name = "Soins palliatifs" },
				new HealthCarePrimary { Name = "Soins et surveillance des personnes diabétiques" },
				new HealthCarePrimary { Name = "Alimentation entérale et parentérale" },
				new HealthCarePrimary { Name = "Pose d'une perfusions" },
				new HealthCarePrimary { Name = "Soins post-chirurgie" },
				new HealthCarePrimary { Name = "Ablation de fils ,agrafes" },
				new HealthCarePrimary { Name = "Ablation dispositifs chirurgicaux" }
			};

			context.HealthCarePrimaries.AddRange(healthCarePrimaries);

			context.SaveChanges();

			var healthCareSecondaries = new HealthCareSecondary[] {
				new HealthCareSecondary { Name = "Aide à la toilette", HealthCarePrimaryId = 1 },
				new HealthCareSecondary { Name = "Toilette complète" , HealthCarePrimaryId = 1 },
				new HealthCareSecondary { Name = "Aide à l'habillage" , HealthCarePrimaryId = 1 },
				new HealthCareSecondary { Name = "Pose et retrait des bas de contention" , HealthCarePrimaryId = 1},
				new HealthCareSecondary { Name = "Sous-cutanées" , HealthCarePrimaryId = 2},
				new HealthCareSecondary { Name = "Intramusculaires" , HealthCarePrimaryId = 2},
				new HealthCareSecondary { Name = "Aucun" , HealthCarePrimaryId = 3},
				new HealthCareSecondary { Name = "Tension, Pouls, Température, Saturation" , HealthCarePrimaryId = 4},
				new HealthCareSecondary { Name = "Avec ou sans réfection des piluliers" , HealthCarePrimaryId = 5},
				new HealthCareSecondary { Name = "Gestion des stocks" , HealthCarePrimaryId = 5},
				new HealthCareSecondary { Name = "simples" , HealthCarePrimaryId = 6},
				new HealthCareSecondary { Name = "complexes" , HealthCarePrimaryId = 6},
				new HealthCareSecondary { Name = "chirurgicaux" , HealthCarePrimaryId = 6},
				new HealthCareSecondary { Name = "infectés" , HealthCarePrimaryId = 6},
				new HealthCareSecondary { Name = "ulcéreux" , HealthCarePrimaryId = 6},
				new HealthCareSecondary { Name = "escarre" , HealthCarePrimaryId = 6},
				new HealthCareSecondary { Name = "Retrait aiguille de hubert" , HealthCarePrimaryId = 7},
				new HealthCareSecondary { Name = "Pose / retrait chimiothérapie" , HealthCarePrimaryId = 7},
				new HealthCareSecondary { Name = "Pose / retrait antibiotique" , HealthCarePrimaryId = 7},
				new HealthCareSecondary { Name = "Pose / retrait hydratation" , HealthCarePrimaryId = 7},
				new HealthCareSecondary { Name = "Autre" , HealthCarePrimaryId = 7},
				new HealthCareSecondary { Name = "Surveillance des paramètres" , HealthCarePrimaryId = 8},
				new HealthCareSecondary { Name = "Administration des traitements per os" , HealthCarePrimaryId = 8},
				new HealthCareSecondary { Name = "Administration des traitements en intraveineux" , HealthCarePrimaryId = 8},
				new HealthCareSecondary { Name = "Surveillance glycémie" , HealthCarePrimaryId = 9},
				new HealthCareSecondary { Name = "Administration insuline" , HealthCarePrimaryId = 9},
				new HealthCareSecondary { Name = "Pose" , HealthCarePrimaryId = 10},
				new HealthCareSecondary { Name = "Retrait" , HealthCarePrimaryId = 10},
				new HealthCareSecondary { Name = "Surveillance" , HealthCarePrimaryId = 10},
				new HealthCareSecondary { Name = "Antibiothérapie" , HealthCarePrimaryId = 11},
				new HealthCareSecondary { Name = "Antidouleur" , HealthCarePrimaryId = 11},
				new HealthCareSecondary { Name = "Hydratation" , HealthCarePrimaryId = 11},
				new HealthCareSecondary { Name = "Autre" , HealthCarePrimaryId = 11},
				new HealthCareSecondary { Name = "Pansements" , HealthCarePrimaryId = 12},
				new HealthCareSecondary { Name = "Injection anti coagulant" , HealthCarePrimaryId = 12},
				new HealthCareSecondary { Name = "Bilan sanguin" , HealthCarePrimaryId = 12},
				new HealthCareSecondary { Name = "Aucun" , HealthCarePrimaryId = 13},
				new HealthCareSecondary { Name = "Aucun" , HealthCarePrimaryId = 14},
			};

			context.HealthCareSecondaries.AddRange(healthCareSecondaries);

			context.SaveChanges();
		}
	}
}