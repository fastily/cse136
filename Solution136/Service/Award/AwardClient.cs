// <copyright file="AwardClient.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.Award
{
    using Microsoft.Practices.Unity;

    using Ninject;

    /// <summary>
    /// Award client to demonstrate different types of dependency injection
    /// </summary>
    public class AwardClient
    {
        /// <summary>
        /// Award scholarship
        /// </summary>
        public void AwardScholarship()
        {
            // Wrote the resolver myself
            var resolver = new Resolver();

            var rankBy1 = resolver.ChooseRanking("undergrad");
            var award1 = new Award(rankBy1);

            award1.AwardScholarship("100");

            var rankBy2 = resolver.ChooseRanking("grad");
            var award2 = new Award(rankBy2);
            award2.AwardScholarship("200");

            // using Ninject instead of the custom resolver I wrote.
            var kernelContainer = new StandardKernel();
            kernelContainer.Bind<IRanking>().To<RankByGPA>();
            var award3 = kernelContainer.Get<Award>();
            award3.AwardScholarship("101");

            kernelContainer.Rebind<IRanking>().To<RankByInnovation>();
            var award4 = kernelContainer.Get<Award>();
            award4.AwardScholarship("201");

            // using Unity instead of custom resolver.
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IRanking, RankByGPA>();
            var award5 = unityContainer.Resolve<Award>();
            award5.AwardScholarship("102");

            unityContainer = new UnityContainer();
            unityContainer.RegisterType<IRanking, RankByInnovation>();
            var award6 = unityContainer.Resolve<Award>();
            award6.AwardScholarship("202");
        }
    }
}