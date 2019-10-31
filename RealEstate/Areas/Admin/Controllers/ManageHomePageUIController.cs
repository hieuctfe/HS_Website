namespace RealEstate.Areas.Admin.Controllers
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using RealEstate.Models.UI;
    using RealEstate.ViewModels.UI;
    using RealEstate.Areas.Admin.Controllers.Shared;
    using RealEstate.ViewModels;

    public class ManageHomePageUIController : BaseAdminController
    {
        #region Banner For BreadCum UI
        [HttpGet]
        public ActionResult UpdateBannerBreadCum()
        {
            UICache bannerField = _context.UICaches.Find(Page.Home, PageComponent.Home_Banner, DataType.Field);
            if (bannerField != null)
            {
                ViewBag.BannerPath = !string.IsNullOrEmpty(bannerField.DataCache) ? JsonConvert.SerializeObject(new List<_ImageCropper>()
                {
                    new _ImageCropper
                    {
                        ImageData = bannerField.DataCache,
                        ImagePath = bannerField.DataCache,
                        ImageName = bannerField.DataCache,
                        IsUploaded = true
                    }
                }) : string.Empty;
                return View();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBannerBreadCum(IEnumerable<HomeUIBannerBreadCumViewModels> slider)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    StringBuilder sb = new StringBuilder();

                    slider.ToList().ForEach(banner =>
                    {
                        sb.Append(SaveImage(banner).ImagePath);
                    });

                    UICache bannerField = _context.UICaches.Find(Page.Home, PageComponent.Home_Banner, DataType.Field);
                    if (bannerField == null)
                    {
                        _context.UICaches.Add(
                            bannerField = new UICache(Page.Home, PageComponent.Home_Banner, DataType.Field));
                    }//create if html type is not existed
                    bannerField.DataCache = sb.ToString();

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateBannerBreadCum));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Banner UI
        [HttpGet]
        public ActionResult UpdateBanner()
        {
            UICache bannerJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Slider, DataType.Json);
            if (bannerJson != null)
            {
                return View(JsonConvert.DeserializeObject<HomeUIViewModels>(bannerJson.DataCache));
            }

            return View(new HomeUIViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBanner(HomeUIViewModels list)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    int index = 0;
                    StringBuilder sb = new StringBuilder();
                    ICollection<HomeUIBannerViewModels> slider = list.Slider;

                    slider.ToList().ForEach(banner =>
                    {
                        SaveImage(banner);

                        ViewBag.Active = index++;
                        sb.Append(RenderPartialViewToString("~/Views/AboutUs/_Slider_Part_Item.cshtml", banner));
                    });

                    UICache bannerHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Slider, DataType.Html);
                    if (bannerHtml == null)
                    {
                        _context.UICaches.Add(
                            bannerHtml = new UICache(Page.Home, PageComponent.Home_Slider, DataType.Html));
                    }//create if html type is not existed
                    bannerHtml.DataCache = sb.ToString();

                    UICache bannerJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Slider, DataType.Json);
                    if (bannerJson == null)
                    {
                        _context.UICaches.Add(
                            bannerJson = new UICache(Page.Home, PageComponent.Home_Slider, DataType.Json));
                    }//create if json type is not existed
                    bannerJson.DataCache = JsonConvert.SerializeObject(
                        new HomeUIViewModels
                        {
                            Slider = slider.Select(x => new HomeUIBannerViewModels
                            {
                                ImagePath = x.ImagePath,
                                ImageName = x.ImageName,
                                IsUploaded = true,

                                Title = x.Title,
                                Content = x.Content,
                                LinkedUrl = x.LinkedUrl,
                                ExtraUrl = x.ExtraUrl
                            }).ToList()
                        });//filter to avoid save image data(base64) to database

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateBanner));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Footer
        [HttpGet]
        public ActionResult UpdateFooter()
        {
            UICache footerHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Footer, DataType.Html);
            if (footerHtml != null)
            {
                return View(new HomeUIFooteViewModels { FooterHtml = footerHtml.DataCache });
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFooter(HomeUIFooteViewModels data)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    UICache footerHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Footer, DataType.Html);
                    if (footerHtml == null)
                    {
                        _context.UICaches.Add(
                            footerHtml = new UICache(Page.Home, PageComponent.Home_Footer, DataType.Html));
                    }
                    footerHtml.DataCache = data.FooterHtml;

                    _context.SaveChanges();
                    transaction.Commit();
                }

                return RedirectToAction(nameof(UpdateFooter));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Offer UI
        [HttpGet]
        public ActionResult UpdateOffer()
        {
            UICache offerJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Offer, DataType.Json);
            if (offerJson != null)
            {
                return View(JsonConvert.DeserializeObject<List<HomeUIOfferViewModels>>(offerJson.DataCache));
            }

            return View(Enumerable.Empty<HomeUIOfferViewModels>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateOffer(List<HomeUIOfferViewModels> list)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    int index = 0;
                    StringBuilder sb = new StringBuilder();

                    list.ForEach(x =>
                    {
                        ViewBag.Active = index++;
                        sb.Append(RenderPartialViewToString("~/Views/AboutUs/_Offer_Part_Item.cshtml", x));
                    });

                    UICache offerHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Offer, DataType.Html);
                    if (offerHtml == null)
                    {
                        _context.UICaches.Add(
                            offerHtml = new UICache(Page.Home, PageComponent.Home_Offer, DataType.Html));
                    }//create if html type is not existed
                    offerHtml.DataCache = sb.ToString();

                    UICache offerJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Offer, DataType.Json);
                    if (offerJson == null)
                    {
                        _context.UICaches.Add(
                            offerJson = new UICache(Page.Home, PageComponent.Home_Offer, DataType.Json));
                    }//create if json type is not existed
                    offerJson.DataCache = JsonConvert.SerializeObject(list);

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateOffer));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Service UI
        [HttpGet]
        public ActionResult UpdateService()
        {
            UICache bannerJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Service, DataType.Json);
            if (bannerJson != null)
            {
                return View(JsonConvert.DeserializeObject<HomeUIServiceViewModels>(bannerJson.DataCache));
            }

            return View(new HomeUIServiceViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateService(HomeUIServiceViewModels service)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    int index = 0;
                    StringBuilder sb = new StringBuilder();

                    sb.Append(RenderPartialViewToString("~/Views/AboutUs/_Service_Section_Container.cshtml", service));
                    sb.Append("<div class='row'>");
                    service.Services.ToList().ForEach(x =>
                    {
                        ViewBag.Active = index++;
                        sb.Append(RenderPartialViewToString("~/Views/AboutUs/_Service_Section_Container_Item.cshtml", x));
                    });
                    sb.Append("</div>");

                    UICache serviceHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Service, DataType.Html);
                    if (serviceHtml == null)
                    {
                        _context.UICaches.Add(
                            serviceHtml = new UICache(Page.Home, PageComponent.Home_Service, DataType.Html));
                    }//create if html type is not existed
                    serviceHtml.DataCache = sb.ToString();

                    UICache serviceJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Service, DataType.Json);
                    if (serviceJson == null)
                    {
                        _context.UICaches.Add(
                            serviceJson = new UICache(Page.Home, PageComponent.Home_Service, DataType.Json));
                    }//create if json type is not existed
                    serviceJson.DataCache = JsonConvert.SerializeObject(service);

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateService));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Achivement UI
        [HttpGet]
        public ActionResult UpdateAchivement()
        {
            UICache achivementJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Achivement, DataType.Json);
            if (achivementJson != null)
            {
                return View(JsonConvert.DeserializeObject<HomeUIAchivementViewModels>(achivementJson.DataCache));
            }

            return View(new HomeUIAchivementViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAchivement(HomeUIAchivementViewModels achivement)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    StringBuilder container = new StringBuilder();
                    StringBuilder elements = new StringBuilder();

                    _ImageCropper backgroundImg = achivement.Background.FirstOrDefault();
                    SaveImage(backgroundImg);
                    container.Append(RenderPartialViewToString("~/Views/AboutUs/_Achivement_Section_Container.cshtml", backgroundImg));

                    achivement.Achivements.ToList().ForEach(x => 
                        elements.Append(RenderPartialViewToString("~/Views/AboutUs/_Achivement_Section_Container_Item.cshtml", x)));

                    container.Replace("{{elements}}", elements.ToString());
                    UICache achivementHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Achivement, DataType.Html);
                    if (achivementHtml == null)
                    {
                        _context.UICaches.Add(
                            achivementHtml = new UICache(Page.Home, PageComponent.Home_Achivement, DataType.Html));
                    }//create if html type is not existed
                    achivementHtml.DataCache = container.ToString();

                    UICache achivementJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Achivement, DataType.Json);
                    if (achivementJson == null)
                    {
                        _context.UICaches.Add(
                            achivementJson = new UICache(Page.Home, PageComponent.Home_Achivement, DataType.Json));
                    }//create if json type is not existed
                    achivementJson.DataCache = JsonConvert.SerializeObject(
                        new
                        {
                            achivement.Achivements,
                            Background = achivement.Background.Select(x => new _ImageCropper
                            {
                                ImagePath = x.ImagePath,
                                ImageName = x.ImageName,
                                IsUploaded = true,
                            }).ToList()
                        });

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateAchivement));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Team UI
        [HttpGet]
        public ActionResult UpdateTeam()
        {
            UICache bannerJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Team, DataType.Json);
            if (bannerJson != null)
            {
                return View(JsonConvert.DeserializeObject<List<HomeUITeamViewModels>>(bannerJson.DataCache));
            }

            return View(Enumerable.Empty<HomeUITeamViewModels>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTeam(List<HomeUITeamViewModels> list)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    StringBuilder sb = new StringBuilder();

                    list.ForEach(x => sb.Append(RenderPartialViewToString("~/Views/AboutUs/_Team_Section_Item.cshtml", 
                                        SaveImage(x))));

                    UICache teamHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Team, DataType.Html);
                    if (teamHtml == null)
                    {
                        _context.UICaches.Add(
                            teamHtml = new UICache(Page.Home, PageComponent.Home_Team, DataType.Html));
                    }//create if html type is not existed
                    teamHtml.DataCache = sb.ToString();

                    UICache teamJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Team, DataType.Json);
                    if (teamJson == null)
                    {
                        _context.UICaches.Add(
                            teamJson = new UICache(Page.Home, PageComponent.Home_Team, DataType.Json));
                    }//create if json type is not existed
                    teamJson.DataCache = JsonConvert.SerializeObject(list.Select(x => 
                        new HomeUITeamViewModels
                        {
                            ImagePath = x.ImagePath,
                            ImageName = x.ImageName,
                            IsUploaded = true,

                            Name = x.Name,
                        }).ToList());//filter to avoid save image data(base64) to database

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateTeam));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Team Property UI
        [HttpGet]
        public ActionResult UpdateTeamProperty()
        {
            UICache bannerJson = _context.UICaches.Find(Page.Property, PageComponent.Home_Team, DataType.Json);
            if (bannerJson != null)
            {
                return View(JsonConvert.DeserializeObject<List<HomeUITeamViewModels>>(bannerJson.DataCache));
            }

            return View(Enumerable.Empty<HomeUITeamViewModels>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTeamProperty(List<HomeUITeamViewModels> list)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    StringBuilder sb = new StringBuilder();

                    list.ForEach(x => sb.Append(RenderPartialViewToString("~/Views/Home/_Team_Sidebar_Item.cshtml",
                                        SaveImage(x))));

                    UICache teamHtml = _context.UICaches.Find(Page.Property, PageComponent.Home_Team, DataType.Html);
                    if (teamHtml == null)
                    {
                        _context.UICaches.Add(
                            teamHtml = new UICache(Page.Property, PageComponent.Home_Team, DataType.Html));
                    }//create if html type is not existed
                    teamHtml.DataCache = sb.ToString();

                    UICache teamJson = _context.UICaches.Find(Page.Property, PageComponent.Home_Team, DataType.Json);
                    if (teamJson == null)
                    {
                        _context.UICaches.Add(
                            teamJson = new UICache(Page.Property, PageComponent.Home_Team, DataType.Json));
                    }//create if json type is not existed
                    teamJson.DataCache = JsonConvert.SerializeObject(list.Select(x =>
                        new HomeUITeamViewModels
                        {
                            ImagePath = x.ImagePath,
                            ImageName = x.ImageName,
                            IsUploaded = true,

                            Name = x.Name,
                        }).ToList());//filter to avoid save image data(base64) to database

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateTeamProperty));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion

        #region Feedback UI
        [HttpGet]
        public ActionResult UpdateClient()
        {
            UICache clientJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Client, DataType.Json);
            if (clientJson != null)
            {
                return View(JsonConvert.DeserializeObject<HomeUIClientViewModels>(clientJson.DataCache));
            }

            return View(new HomeUIClientViewModels());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateClient(HomeUIClientViewModels client)
        {
            try
            {
                using (DbContextTransaction transaction = _context.Database.BeginTransaction())
                {
                    StringBuilder container = new StringBuilder();
                    StringBuilder elements = new StringBuilder();

                    _ImageCropper backgroundImg = client.Background.FirstOrDefault();
                    container.Append(RenderPartialViewToString("~/Views/AboutUs/_Client_Feedback_Container.cshtml", SaveImage(backgroundImg)));

                    client.Clients.ToList().ForEach(x =>
                        elements.Append(RenderPartialViewToString("~/Views/AboutUs/_Client_Feedback_Container_Item.cshtml", SaveImage(x))));

                    container.Replace("{{elements}}", elements.ToString());
                    UICache achivementHtml = _context.UICaches.Find(Page.Home, PageComponent.Home_Client, DataType.Html);
                    if (achivementHtml == null)
                    {
                        _context.UICaches.Add(
                            achivementHtml = new UICache(Page.Home, PageComponent.Home_Client, DataType.Html));
                    }//create if html type is not existed
                    achivementHtml.DataCache = container.ToString();

                    UICache achivementJson = _context.UICaches.Find(Page.Home, PageComponent.Home_Client, DataType.Json);
                    if (achivementJson == null)
                    {
                        _context.UICaches.Add(
                            achivementJson = new UICache(Page.Home, PageComponent.Home_Client, DataType.Json));
                    }//create if json type is not existed
                    achivementJson.DataCache = JsonConvert.SerializeObject(
                        new HomeUIClientViewModels
                        {
                            Clients = client.Clients.Select(x => new ClientViewModels
                            {
                                ImagePath = x.ImagePath,
                                ImageName = x.ImageName,
                                IsUploaded = true,

                                Name = x.Name,
                                Position = x.Position,
                                Content = x.Content,
                            }).ToList(),
                            Background = client.Background.Select(x => new _ImageCropper
                            {
                                ImagePath = x.ImagePath,
                                ImageName = x.ImageName,
                                IsUploaded = true,
                            }).ToList()
                        });

                    _context.SaveChanges();
                    transaction.Commit();
                }//end of transaction

                return RedirectToAction(nameof(UpdateClient));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
        #endregion
    }
}