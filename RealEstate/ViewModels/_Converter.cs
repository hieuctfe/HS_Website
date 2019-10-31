namespace RealEstate.ViewModels
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using RealEstate.Models;
    using RealEstate.Models.ComplexType;
    using RealEstate.Infrastructure;
    using RealEstate.Models.Identity;

    public class _Converter
    {
        #region Profile
        public ProfileViewModels CreateProfileViewModels(Account user)
            => Map(user, x => new ProfileViewModels
            {
                Username = x.UserName,
                Fullname = x.FullName,
                EmailAddress = x.Email,
                PhoneNumber = x.PhoneNumber,
                Avatar = new List<_ImageCropper>
                {
                    new _ImageCropper
                    {
                        IsUploaded = true,
                        ImageName = "avatar",
                        ImagePath = x.Avatar,
                        ImageData = x.Avatar
                    }
                }
            });
        #endregion

        #region Properties
        /// <summary>
        /// Need to include PropertyImages, PropertyStatusCode, PropertyType, Comments
        /// </summary>
        public PropertyDetailViewModels CreatePropertyDetailViewModels(Property property)
             => Map(property, x => new PropertyDetailViewModels
             {
                 Id = x.Id,
                 Name = x.Name,
                 Address = x.AddressLine,
                 DetailDescription = x.DetailDescription,
                 Area = x.Area,
                 City = x.City,
                 Code = x.Code,
                 CreatedOn = x.ActivityLog.CreatedOn,
                 District = x.District,
                 HasGarden = x.HasGarden,
                 HasSwimming = x.HasSwimming,
                 HeaderImages = x.PropertyImages.Select(y => (y.Name, y.Path)),
                 NumberOfBathRoom = x.NumberOfBathRoom,
                 ContactAddress = x.ContactAddress,
                 ContactEmail = x.ContactEmail,
                 ContactName = x.ContactName,
                 ContactPhone = x.ContactPhoneNumber,
                 NumberOfBedRoom = x.NumberOfBedRoom,
                 NumberOfGarage = x.NumberOfGarage,
                 Price = x.Price,
                 Status = x.PropertyStatusCode.Name,
                 Type = x.PropertyType.Name,
                 Comments = CreateCommentViewModel(x.Comments.Where(z => z.IsVerify)),
                 Rating = Math.Round((double)x.Comments.Sum(y => y.Rating) / x.Comments.Count)
             });

        /// <summary>
        /// Id, AvatarName, AvatarPath, PropertyImages, DisplayOrder is not modified
        /// </summary>
        public void UpdateProperty(UpdatePropertyViewModels entity, Property property)
            => Map(entity, property, (x, y) =>
            {
                y.Code = x.BasicInformation.Code;
                y.Name = x.BasicInformation.Name;
                y.Price = x.BasicInformation.Price;
                y.Area = x.BasicInformation.Area;
                y.AddressLine = x.BasicInformation.AddressLine;
                y.City = x.BasicInformation.City;
                y.District = x.BasicInformation.District;
                y.PostTitle = x.BasicInformation.PostTitle;
                y.ShortDescription = x.BasicInformation.ShortDescription;
                y.DetailDescription = x.BasicInformation.DetailDescription;

                y.HasCarGarage = x.BasicInformation.HasCarGarage;
                y.HasGarden = x.BasicInformation.HasGarden;
                y.HasSwimming = x.BasicInformation.HasSwimming;
                y.IsFeatured = x.BasicInformation.IsFeatured;

                y.ContactAddress = x.ContactInformation.ContactAddress;
                y.ContactEmail = x.ContactInformation.ContactEmail;
                y.ContactName = x.ContactInformation.ContactName;
                y.ContactPhoneNumber = x.ContactInformation.ContactPhoneNumber;

                y.NumberOfBathRoom = x.BasicInformation.NumberOfBathRoom;
                y.NumberOfBedRoom = x.BasicInformation.NumberOfBedRoom;
                y.NumberOfGarage = x.BasicInformation.HasCarGarage ? x.BasicInformation.NumberOfGarage : 0;

                y.PropertyTypeId = x.BasicInformation.PropertyTypeId;
                y.PropertyStatusCodeId = x.BasicInformation.PropertyStatusCodeId;

                y.ActivityLog.ModifiedOn = DateTimeOffset.Now;
                y.ActivityLog.ModifiedBy = "Unknown";

                y.UIOption.IsDisplay = x.BasicInformation.IsDisplay;

                y.Seo = new Seo
                {
                    Title = x.Seo.Title,
                    AliasName = x.Seo.AliasName,
                    FriendlyUrl = x.Seo.FriendlyUrl.RewriteUrl(),
                    MetaContent = x.Seo.MetaContent,
                    MetaDescription = x.Seo.MetaDescription,
                    StructureData = x.Seo.StructureData,
                };
            });

        /// <summary>
        /// Need to include PropertyImages
        /// </summary>
        public UpdatePropertyViewModels CreateUpdatePropertyViewModels(Property property)
            => Map(property, x => new UpdatePropertyViewModels
            {
                Avatar = new List<_ImageCropper>
                {
                    new _ImageCropper
                    {
                        IsUploaded = true,
                        ImageName = x.AvatarName,
                        ImagePath = x.AvatarPath,
                        ImageData = x.AvatarPath,
                    }
                },
                PropertyImages = x.PropertyImages.Select(y =>
                    new _ImageCropper
                    {
                        IsUploaded = true,
                        ImageName = y.Name,
                        ImagePath = y.Path,
                        ImageData = y.Path,
                    }).ToList(),

                BasicInformation = new PropertyBasicInformation
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    AddressLine = x.AddressLine,
                    Price = x.Price,
                    Area = x.Area,
                    City = x.City,
                    District = x.District,
                    PostTitle = x.PostTitle,
                    ShortDescription = x.ShortDescription,
                    DetailDescription = x.DetailDescription,

                    HasCarGarage = x.HasCarGarage,
                    HasGarden = x.HasGarden,
                    HasSwimming = x.HasSwimming,

                    IsDisplay = x.UIOption.IsDisplay,
                    IsFeatured = x.IsFeatured,

                    NumberOfBathRoom = x.NumberOfBathRoom,
                    NumberOfBedRoom = x.NumberOfBedRoom,
                    NumberOfGarage = x.NumberOfGarage,

                    PropertyStatusCodeId = x.PropertyStatusCodeId,
                    PropertyTypeId = x.PropertyTypeId
                },

                ContactInformation = new PropertyContacInformation
                {
                    ContactAddress = x.ContactAddress,
                    ContactPhoneNumber = x.ContactPhoneNumber,
                    ContactName = x.ContactName,
                    ContactEmail = x.ContactEmail
                },

                Seo = x.Seo,
                ActivityLog = x.ActivityLog,
                UIOption = x.UIOption
            });

        /// <summary>
        /// AvatarName, AvatarPath, PropertyImages is not init
        /// </summary>
        public Property CreateProperty(CreatePropertyViewModels entity)
            => Map(entity, x => new Property
            {
                AvatarName = string.Empty,
                AvatarPath = string.Empty,
                PropertyImages = new List<PropertyImage>(),

                Code = x.BasicInformation.Code,
                Name = x.BasicInformation.Name,
                Price = x.BasicInformation.Price,
                Area = x.BasicInformation.Area,
                AddressLine = x.BasicInformation.AddressLine,
                City = x.BasicInformation.City,
                District = x.BasicInformation.District,
                PostTitle = x.BasicInformation.PostTitle,
                ShortDescription = x.BasicInformation.ShortDescription,
                DetailDescription = x.BasicInformation.DetailDescription,

                ContactAddress = x.ContactInformation.ContactAddress,
                ContactEmail = x.ContactInformation.ContactEmail,
                ContactName = x.ContactInformation.ContactName,
                ContactPhoneNumber = x.ContactInformation.ContactPhoneNumber,

                HasCarGarage = x.BasicInformation.HasCarGarage,
                HasGarden = x.BasicInformation.HasGarden,
                HasSwimming = x.BasicInformation.HasSwimming,

                IsFeatured = x.BasicInformation.IsFeatured,

                NumberOfBathRoom = x.BasicInformation.NumberOfBathRoom,
                NumberOfBedRoom = x.BasicInformation.NumberOfBedRoom,
                NumberOfGarage = x.BasicInformation.HasCarGarage ? x.BasicInformation.NumberOfGarage : 0,

                PropertyTypeId = x.BasicInformation.PropertyTypeId,
                PropertyStatusCodeId = x.BasicInformation.PropertyStatusCodeId,

                Seo = x.Seo,
                UIOption = new UIOption
                {
                    IsDisplay = x.BasicInformation.IsDisplay,
                    DisplayOrder = x.UIOption.DisplayOrder
                },
                ActivityLog = x.ActivityLog,
            });

        /// <summary>
        /// Need to include PropertyStatusCode and PropertyType
        /// </summary>
        public PropertyViewModels CreatePropertyViewModels(Property property)
            => Map(property, x => new PropertyViewModels
            {
                Address = x.AddressLine,
                Area = x.Area,
                AvatarName = x.AvatarName,
                AvatarPath = x.AvatarPath,
                City = x.City,
                Code = x.Code,
                District = x.District,
                Id = x.Id,
                Name = x.Name,
                NumberOfBathRoom = x.NumberOfBathRoom,
                NumberOfBedRoom = x.NumberOfBedRoom,
                NumberOfGarage = x.NumberOfGarage,
                HasGarage = x.HasCarGarage,
                Price = x.Price,
                ContactPhone = x.ContactPhoneNumber,
                Status = x.PropertyStatusCode.Name,
                Type = x.PropertyType.Name,
                IsFeatured = x.IsFeatured,
                IsDisplay = x.UIOption.IsDisplay,
                CreatedOn = x.ActivityLog.CreatedOn,
                OrderStatus = x.OrderStatus.GetDescription()
            });

        /// <summary>
        /// Need to include PropertyStatusCode and PropertyType
        /// </summary>
        public IEnumerable<PropertyViewModels> CreatePropertyViewModels(IEnumerable<Property> properties)
            => Map(properties, x => x.Select(y => CreatePropertyViewModels(y)));
        #endregion

        #region Post
        /// <summary>
        /// Need to include Comments, PostLabelDatas, PostLabel
        /// </summary>
        public PostDetailViewModels CreatePostDetailViewModels(Post post)
            => Map(post, x => new PostDetailViewModels
            {
                Id = x.Id,
                Name = x.Name,
                Content = x.Content,
                CreatedBy = x.ActivityLog.CreatedBy,
                CreatedOn = x.ActivityLog.CreatedOn,
                Labels = x.PostLabelDatas.Select(y => new SharedPostLabelViewModels
                {
                    Id = y.PostLabelId,
                    Name = y.PostLabel.Name
                }),
                Description = x.Description,
                HeaderImageName = x.HeaderImageName,
                HeaderImagePath = x.HeaderImagePath,
                Comments = CreateCommentViewModel(x.Comments.Where(z => z.IsVerify))
            });

        /// <summary>
        /// AvatarName, AvatarPath, HeaderImageName, HeaderImagePath in not init
        /// </summary>
        public Post CreatePost(CreatePostViewModels entity)
            => Map(entity, x => new Post
            {
                ActivityLog = x.ActivityLog,
                Seo = new Seo
                {
                    Title = x.Seo.Title,
                    AliasName = x.Seo.AliasName,
                    FriendlyUrl = x.Seo.FriendlyUrl,
                    StructureData = x.Seo.StructureData,
                    MetaContent = x.Seo.MetaContent,
                    MetaDescription = x.Seo.MetaDescription
                },
                UIOption = new UIOption
                {
                    IsDisplay = x.BasicInformation.IsDisplay,
                    DisplayOrder = x.UIOption.DisplayOrder,
                },
                Content = x.BasicInformation.Content,
                Description = x.BasicInformation.Description,
                Name = x.BasicInformation.Name,
                PostCategoryId = x.BasicInformation.PostCategoryId,
                PostLabelDatas = x.BasicInformation.PostLabelIds.Select(y => new PostLabelData
                {
                    PostLabelId = y
                }).ToList()
            });

        /// <summary>
        /// Need to include PostLabelDatas
        /// </summary>
        public UpdatePostViewModels CreateUpdatePostViewModels(Post entity)
            => Map(entity, x => new UpdatePostViewModels
            {
                Avatar = new List<_ImageCropper>
                {
                    new _ImageCropper
                    {
                        IsUploaded = true,
                        ImageName = x.AvatarName,
                        ImagePath = x.AvatarPath,
                        ImageData = x.AvatarPath
                    }
                },
                HeaderImage = new List<_ImageCropper>
                {
                    new _ImageCropper
                    {
                        IsUploaded = true,
                        ImageName = x.HeaderImageName,
                        ImagePath = x.HeaderImagePath,
                        ImageData = x.HeaderImagePath
                    }
                },
                BasicInformation = new PostBasicInformation
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Content = x.Content,

                    PostLabelIds = x.PostLabelDatas.Select(y => y.PostLabelId).ToList(),
                    PostCategoryId = x.PostCategoryId,

                    IsDisplay = x.UIOption.IsDisplay
                },
                Seo = x.Seo,
                UIOption = x.UIOption,
                ActivityLog = x.ActivityLog
            });

        /// <summary>
        /// Id, AvatarName, AvatarPath, HeaderImageName, HeaderImagePath, DisplayOrder, PostLabelDatas is not modified
        /// </summary>
        public void UpdatePost(UpdatePostViewModels entity, Post post)
            => Map(entity, post, (x, y) =>
            {
                y.Name = x.BasicInformation.Name;
                y.Description = x.BasicInformation.Description;
                y.Content = x.BasicInformation.Content;

                y.PostCategoryId = x.BasicInformation.PostCategoryId;

                y.ActivityLog.ModifiedOn = DateTimeOffset.Now;
                y.ActivityLog.ModifiedBy = "Unknown";

                y.UIOption.IsDisplay = x.BasicInformation.IsDisplay;

                y.Seo = new Seo
                {
                    Title = x.Seo.Title,
                    AliasName = x.Seo.AliasName,
                    FriendlyUrl = x.Seo.FriendlyUrl.RewriteUrl(),
                    MetaContent = x.Seo.MetaContent,
                    MetaDescription = x.Seo.MetaDescription,
                    StructureData = x.Seo.StructureData,
                };
            });

        /// <summary>
        /// Need to include comments
        /// </summary>
        public PostViewModels CreatePostViewModels(Post post)
            => Map(post, x => new PostViewModels
            {
                Id = x.Id,
                Name = x.Name,
                AvatarName = x.AvatarName,
                AvatarPath = x.AvatarPath,
                Description = x.Description,

                CommentCount = x.Comments.Count,
                ViewCount = x.ViewCount,

                CreatedOn = x.ActivityLog.CreatedOn,
                CreatedBy = x.ActivityLog.CreatedBy,
                IsDisplay = x.UIOption.IsDisplay,
            });

        /// <summary>
        /// Need to include comments
        /// </summary>
        public IEnumerable<PostViewModels> CreatePostViewModels(IEnumerable<Post> posts)
            => Map(posts, x => x.Select(y => CreatePostViewModels(y)));
        #endregion

        #region Comment
        public Comment CreateComment(CommentViewModels comment)
            => Map(comment, x => new Comment
            {
                Owner = x.Owner,
                Description = x.Description,
                EmailAddress = x.EmailAddress,
                Rating = x.Rating,

                PostId = x.PostId,
                PropertyId = x.PropertyId,
            });

        public CommentViewModels CreateCommentViewModel(Comment comment)
            => Map(comment, x => new CommentViewModels
            {
                Owner = x.Owner,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
                EmailAddress = x.EmailAddress,
                Rating = x.Rating,

                Id = x.Id,
                PropertyId = x.PropertyId,
                PostId = x.PostId,
                ParentId = x.ParentId,

                Child = CreateCommentViewModel(x.Childrens ?? Enumerable.Empty<Comment>()).ToList()
            });

        public IEnumerable<CommentViewModels> CreateCommentViewModel(IEnumerable<Comment> comments)
            => Map(comments, x => comments.Select(y => CreateCommentViewModel(y)));

        public ReadCommentViewModels CreateReadCommentViewModes(Comment comment, string _rootUrl)
            => Map(comment, x => new ReadCommentViewModels
            {
                Id = x.Id,
                Owner = x.Owner,
                EmailAddress = x.EmailAddress,
                Content = x.Description,
                Rating = x.Rating,
                IsVerify = x.IsVerify,
                CreatedOn = x.CreatedOn,
                UrlLinked = x.PostId.HasValue ? $"{_rootUrl}Blog/Detail/{x.PostId}"
                                              : $"{_rootUrl}Home/Detail/{x.PropertyId}",
            });

        public IEnumerable<ReadCommentViewModels> CreateReadCommentViewModels(IEnumerable<Comment> comments, string _rootUrl)
            => Map(comments, x => comments.Select(y => CreateReadCommentViewModes(y, _rootUrl)));
        #endregion

        #region Categories and Labels
        public SharedPostCategoryViewModels CreatePostCategory(PostCategory category)
            => Map(category, x => new SharedPostCategoryViewModels
            {
                Id = x.Id,
                Name = x.Name
            });

        public SharedPostLabelViewModels CreatePostLabel(PostLabel label)
            => Map(label, x => new SharedPostLabelViewModels
            {
                Id = x.Id,
                Name = x.Name
            });

        public IEnumerable<SharedPostCategoryViewModels> CreatePostCategories(IEnumerable<PostCategory> categories)
            => Map(categories, x => x.Select(y => CreatePostCategory(y)));

        public IEnumerable<SharedPostLabelViewModels> CreatePostLabels(IEnumerable<PostLabel> labels)
            => Map(labels, x => x.Select(y => CreatePostLabel(y)));
        #endregion

        #region aspect oriented programming 
        private TResultModel Map<TResultModel, TSourceModel>(TSourceModel source, Func<TSourceModel, TResultModel> viewModels)
            where TResultModel : class
            where TSourceModel : class
        {
            TResultModel result = null;
            try
            {
                result = viewModels?.Invoke(source);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private void Map<TResultModel, TSourceModel>(TSourceModel source, TResultModel result, Action<TSourceModel, TResultModel> map)
            where TResultModel : class
            where TSourceModel : class
        {
            try
            {
                map?.Invoke(source, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}