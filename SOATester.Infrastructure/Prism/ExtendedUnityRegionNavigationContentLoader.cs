using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Common;
using Prism.Regions;
using SOATester.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace SOATester.Infrastructure.Prism {
    public class ExtendedUnityRegionNavigationContentLoader : IRegionNavigationContentLoader {
        private readonly IServiceLocator serviceLocator;
        private readonly IUnityContainer container;

        public ExtendedUnityRegionNavigationContentLoader(IServiceLocator serviceLocator, IUnityContainer container) {
            this.serviceLocator = serviceLocator;
            this.container = container;
        }

        public object LoadContent(IRegion region, NavigationContext navigationContext) {
            if (region == null) {
                throw new ArgumentNullException("region");
            }
            if (navigationContext == null) {
                throw new ArgumentNullException("navigationContext");
            }

            string candidateTargetContract = GetContractFromNavigationContext(navigationContext);
            var candidates = GetCandidatesFromRegion(region, candidateTargetContract);
            var acceptingCandidates =
                candidates.Where(
                    v => {
                        var navigationAware = v as INavigationAware;
                        if (navigationAware != null && !navigationAware.IsNavigationTarget(navigationContext)) {
                            return false;
                        }

                        var frameworkElement = v as FrameworkElement;
                        if (frameworkElement == null) {
                            return true;
                        }

                        navigationAware = frameworkElement.DataContext as INavigationAware;
                        return navigationAware == null || navigationAware.IsNavigationTarget(navigationContext);
                    });


            var view = acceptingCandidates.FirstOrDefault();

            if (view != null) {
                return view;
            }

            view = CreateNewRegionItem(candidateTargetContract, navigationContext);

            region.Add(view);

            return view;
        }

        protected virtual object CreateNewRegionItem(string candidateTargetContract, NavigationContext context) {
            object newRegionItem;

            try {
                newRegionItem = serviceLocator.GetInstance<object>(candidateTargetContract);

                var fwElem = newRegionItem as FrameworkElement;

                if (fwElem == null) {
                    return newRegionItem;
                }

                var navVm = fwElem.DataContext as INavigableViewModel;

                if (navVm == null) {
                    return newRegionItem;
                }

                navVm.OnBeforeNavigation(context);

                return newRegionItem;
            } catch (ActivationException e) {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot create navigation target {0}", candidateTargetContract), e);
            }
        }

        protected virtual string GetContractFromNavigationContext(NavigationContext navigationContext) {
            if (navigationContext == null) {
                throw new ArgumentNullException("navigationContext");
            }

            var candidateTargetContract = UriParsingHelper.GetAbsolutePath(navigationContext.Uri);

            candidateTargetContract = candidateTargetContract.TrimStart('/');

            return candidateTargetContract;
        }

        protected virtual IEnumerable<object> BaseGetCandidatesFromRegion(IRegion region, string candidateNavigationContract) {
            if (region == null) {
                throw new ArgumentNullException("region");
            }

            return region.Views.Where(v =>
                string.Equals(v.GetType().Name, candidateNavigationContract, StringComparison.Ordinal) ||
                string.Equals(v.GetType().FullName, candidateNavigationContract, StringComparison.Ordinal));
        }

        protected IEnumerable<object> GetCandidatesFromRegion(IRegion region, string candidateNavigationContract) {
            if (candidateNavigationContract == null || candidateNavigationContract.Equals(string.Empty)) {
                throw new ArgumentNullException("candidateNavigationContract");
            }

            IEnumerable<object> contractCandidates = BaseGetCandidatesFromRegion(region, candidateNavigationContract);

            if (!contractCandidates.Any()) {
                var matchingRegistration = container.Registrations.Where(r => candidateNavigationContract.Equals(r.Name, StringComparison.Ordinal)).FirstOrDefault();
                if (matchingRegistration == null) {
                    matchingRegistration = container.Registrations.Where(r => candidateNavigationContract.Equals(r.RegisteredType.Name, StringComparison.Ordinal)).FirstOrDefault();
                }
                if (matchingRegistration == null) {
                    return new object[0];
                }

                string typeCandidateName = matchingRegistration.MappedToType.FullName;

                contractCandidates = BaseGetCandidatesFromRegion(region, typeCandidateName);
            }

            return contractCandidates;
        }
    }
}
