'use strict';
window.MyLib = window.MyLib || {};

// Revealing module 
MyLib = (function ($) {

    var URL = window.URL || window.webkitURL,
        $CURRENT_INPUT = null,
        currentSrc = null,
        originalImageURL = '',
        uploadedImageName = '',
        uploadedImageType = 'image/jpeg',
        uploadedImageURL;

    var render = {
        imageItem: function () {
            var self = this;

            self.$Container.empty();
            for (let i = 0; i < self.ImageList.length; i++) {
                let index = self.IsSingle ? self.DefaultIndex : i;
                var template = '';

                template +=
                    `<div class="col-2 image-item position-relative">
                        <img data-id="${i}" style="max-width: 100%" src="${self.ImageList[i].src}" />
                        <button type="button" data-action="delete" data-id="${i}" class="btn btn-warning btn-delete position-absolute">
                            <i class="fa fa-times"></i>
                        </button>
                        <input type="hidden" name="${self.InputName}[${index}].IsUploaded" value="${self.ImageList[i].isUploaded}">
                        <input type="hidden" name="${self.InputName}[${index}].ImageName" value="${self.ImageList[i].name}">
                        <input type="hidden" name="${self.InputName}[${index}].ImagePath" value="${self.ImageList[i].path}">`;
                //if (!self.ImageList[i].isUploaded) {
                    template += `<input type="hidden" name="${self.InputName}[${index}].ImageData" value="${self.ImageList[i].src}"></div>`;
                //}
                self.$Container.append(template);
            }
        }
    };

    var helper = {
        toDataURL: function (url, callback) {
            var image = new Image();
            image.crossOrigin = 'Anonymous';
            image.onload = function () {
                var canvas = document.createElement('canvas');
                canvas.width = this.naturalWidth;
                canvas.height = this.naturalHeight;
                canvas.getContext('2d').drawImage(this, 0, 0);
                callback(canvas.toDataURL());

            };
            image.src = url;
        }
    };

    //CropperModal
    function CropperModal(opt) {
        var self = this;

        self.$Modal = $(opt.$croperModal);
        if (self.$Modal.length > 0) {
            self.$Image = self.$Modal.find('#image');
            self.$InputImage = self.$Modal.find('#inputImage');
            self.$DataX = self.$Modal.find('#dataX');
            self.$DataY = self.$Modal.find('#dataY');
            self.$DataHeight = self.$Modal.find('#dataHeight');
            self.$DataWidth = self.$Modal.find('#dataWidth');
            self.$DataRotate = self.$Modal.find('#dataRotate');
            self.$DataScaleX = self.$Modal.find('#dataScaleX');
            self.$DataScaleY = self.$Modal.find('#dataScaleY');
            self.$Background = self.$Modal.find('.no-image-background');
            self.ImageList = [];

            self.options = {
                aspectRatio: 1 / 1,
                preview: '.img-preview',
                minContainerWidth: 557,
                minContainerHeight: 300,
                crop: function (e) {
                    self.$DataX.val(Math.round(e.detail.x));
                    self.$DataY.val(Math.round(e.detail.y));
                    self.$DataHeight.val(Math.round(e.detail.height));
                    self.$DataWidth.val(Math.round(e.detail.width));
                    self.$DataRotate.val(e.detail.rotate);
                    self.$DataScaleX.val(e.detail.scaleX);
                    self.$DataScaleY.val(e.detail.scaleY);
                }
            };
        }
    }

    CropperModal.prototype = {
        init: function () {
            var self = this;
            originalImageURL = self.$Image.attr('src');

            // Tooltip
            self.$Modal.find('[data-toggle="tooltip"]').tooltip();

            // Cropper
            self.$Image.on({
                ready: function (e) { },
                cropstart: function (e) { },
                cropmove: function (e) { },
                cropend: function (e) { },
                crop: function (e) { },
                zoom: function (e) { }
            }).cropper(self.options);

            // Buttons
            if (!$.isFunction(document.createElement('canvas').getContext)) {
                $('button[data-method="getCroppedCanvas"]').prop('disabled', true);
            }

            if (typeof document.createElement('cropper').style.transition === 'undefined') {
                $('button[data-method="rotate"]').prop('disabled', true);
                $('button[data-method="scale"]').prop('disabled', true);
            }

            //Options
            self.$Modal.find('.docs-toggles').on('change', 'input', function () {
                var $this = $(this),
                    name = $this.attr('name'),
                    type = $this.prop('type'),
                    cropBoxData, canvasData;

                if (!self.$Image.data('cropper')) { return; }
                if (type === 'checkbox') {
                    self.options[name] = $this.prop('checked');
                    cropBoxData = self.$Image.cropper('getCropBoxData');
                    canvasData = self.$Image.cropper('getCanvasData');

                    self.options.ready = function () {
                        self.$Image.cropper('setCropBoxData', cropBoxData);
                        self.$Image.cropper('setCanvasData', canvasData);
                    };
                } else if (type === 'radio') {
                    self.options[name] = $this.val();
                }

                self.options.ready = undefined;
                self.$Image.cropper('destroy').cropper(self.options);
            });

            //Methods
            self.$Modal.find('.docs-buttons').on('click', '[data-method]', function () {
                var $this = $(this),
                    data = $this.data(),
                    cropper = self.$Image.data('cropper'),
                    cropped, result, $target;

                self.options.ready = undefined;
                if ($this.prop('disabled') || $this.hasClass('disabled')) { return; }

                if (cropper && data.method) {
                    data = $.extend({}, data); // Clone a new one
                    if (typeof data.target !== 'undefined') {
                        $target = $(data.target);
                        if (typeof data.option === 'undefined') {
                            try {
                                data.option = JSON.parse($target.val());
                            } catch (e) {
                                console.log(e.message);
                            }
                        }
                    }

                    cropped = cropper.cropped;
                    switch (data.method) {
                        case 'rotate':
                            if (cropped && self.options.viewMode > 0) {
                                self.$Image.cropper('clear');
                            }
                            break;

                        case 'getCroppedCanvas':
                            if (uploadedImageType === 'image/jpeg') {
                                if (!data.option) {
                                    data.option = {};
                                }
                                data.option.fillColor = '#fff';
                            }
                            break;
                    }

                    result = self.$Image.cropper(data.method, data.option, data.secondOption);
                    switch (data.method) {
                        case 'rotate': if (cropped && self.options.viewMode > 0) { self.$Image.cropper('crop'); } break;
                        case 'scaleX':
                        case 'scaleY': $(this).data('option', -data.option); break;
                        case 'getCroppedCanvas':
                            if (result) {
                                var id = self.$Modal.attr('data-id');
                                if (!id) {
                                    self.ImageList.push({
                                        isUploaded: false,
                                        name: uploadedImageName,
                                        path: '',
                                        src: result.toDataURL(uploadedImageType),
                                        data: self.$Image.data('cropper').getData(),
                                        full: currentSrc

                                    });
                                } else {
                                    self.ImageList[id].src = result.toDataURL(uploadedImageType);
                                    self.ImageList[id].data = self.$Image.data('cropper').getData(),
                                        self.ImageList[id].isUploaded = false;
                                }
                                //console.log($CURRENT_INPUT);
                                render.imageItem.call($CURRENT_INPUT);
                                self.$Modal.modal('hide');
                                //console.log('-------------------')
                                //console.log(self.ImageList);
                            }
                            break;

                        case 'destroy':
                            if (uploadedImageURL) {
                                URL.revokeObjectURL(uploadedImageURL);
                                uploadedImageURL = '';
                                self.$Image.attr('src', originalImageURL);
                                self.$Background.fadeIn();
                            }
                            break;
                    }

                    if ($.isPlainObject(result) && $target) {
                        try {
                            $target.val(JSON.stringify(result));
                        } catch (e) {
                            console.log(e.message);
                        }
                    }
                }
            });

            //Keyboard
            //$(document.body).on('keydown', function (e) {
            //    if (!self.$Image.data('cropper') || this.scrollTop > 300) { return; }
            //    switch (e.which) {
            //        case 37: self.$Image.cropper('move', -1, 0); break;
            //        case 38: self.$Image.cropper('move', 0, -1); break;
            //        case 39: self.$Image.cropper('move', 1, 0); break;
            //        case 40: self.$Image.cropper('move', 0, 1); break;
            //    }
            //    e.preventDefault();
            //});

            if (URL) {
                self.$InputImage.change(function () {
                    var files = this.files;
                    
                    if (!self.$Image.data('cropper')) { self.$Background.fadeIn(); return; }


                    if (files && files.length) {
                        var file = files[0];

                        if (/^image\/\w+$/.test(file.type)) {
                            if (self.ImageList.length < 100) {
                                uploadedImageName = file.name;
                                uploadedImageType = file.type;

                                var reader = new FileReader();
                                reader.onload = function (e) { currentSrc = e.target.result; };
                                reader.readAsDataURL(file);

                                if (uploadedImageURL) { URL.revokeObjectURL(uploadedImageURL); }
                                uploadedImageURL = URL.createObjectURL(file);
                                self.options.ready = undefined;
                                self.$Image.cropper('destroy').attr('src', uploadedImageURL).cropper(self.options);
                                self.$Background.fadeOut();
                                self.$InputImage.val('');
                                self.$Modal.attr('data-id', null);
                                self.$Modal.modal('show');
                            } else {
                                swal(``, `Chọn Tối Đa ${100} Hình`, `warning`);
                            }

                        } else {
                            self.$Background.fadeIn();
                            swal(``, `Vui Lòng Chọn Tập Tin Với Định Dạng Hình Ảnh`, `error`);
                        }
                    }
                });
            } else {
                self.CropperModal.$InputImage.prop('disabled', true).parent().addClass('disabled');
            }
            return self;
        },
        setState: function (imageList, aspectRatio) {
            var self = this;
            self.ImageList = imageList;
            self.options.aspectRatio = aspectRatio;
        }
    };

    //CropperImages
    function CropperImages(opt) {
        var self = this;

        self.$ImageUpload = $(opt.$ImageUpload);
        self.$Container = $(self.$ImageUpload.attr('data-image-container'));
        self.CropperModal = opt.CropperModalInstance;
        self.MaxNumber = opt.MaxNumber || 1;
        self.AspectRatio = opt.AspectRatio || NaN;
        self.DefaultIndex = opt.DefaultIndex || 0;
        self.IsSingle = opt.IsSingle || false;
        self.InputName = opt.InputName;
        self.ImageList = [];
    }

    CropperImages.prototype = {
        init: function () {
            var self = this;
            if (self.IsSingle) { self.MaxNumber = 1; }

            // Import image
            self.$ImageUpload.on('click', function () {
                $CURRENT_INPUT = self;
                if (self.ImageList.length < self.MaxNumber) {
                    self.CropperModal.$InputImage.trigger("click");
                    self.CropperModal.setState(self.ImageList, self.AspectRatio);
                } else {
                    swal(``, `Chọn Tối Đa ${self.MaxNumber} Hình`, `warning`);
                }
            });

            self.$Container.on('click', 'button[data-action="delete"]', function () {
                $CURRENT_INPUT = self;
                self.ImageList.splice($(this).data('id'), 1);
                self.CropperModal.setState(self.ImageList, self.AspectRatio);
                render.imageItem.call(self);
            });

            self.$Container.on('click', 'img', function () {
                $CURRENT_INPUT = self;
                var index = $(this).data('id');
                if (isNaN(index) && !index) { return; }
                self.CropperModal.$Modal.attr('data-id', index);
                self.CropperModal.setState(self.ImageList, self.AspectRatio);
                self.CropperModal.options.ready = function () {
                    if (typeof self.ImageList[index].data !== 'undefined') {
                        this.cropper.setData(self.ImageList[index].data);
                    }
                    self.CropperModal.$Modal.modal('show');
                };
                self.CropperModal.$Image.cropper('destroy').attr('src', self.ImageList[index].full).cropper(self.CropperModal.options);
                //console.log('-------------------')
                //console.log(self.ImageList);
                //console.log(self.CropperModal.ImageList);
            });

            self.load();
            return self;
        },
        load: function () {
            var self = this;
            var images = JSON.parse(self.$ImageUpload.find('[name="uploadedData"]').val() || '[]');
            images.forEach(function (item, index) {
                var image = {
                    isUploaded: item.IsUploaded,
                    name: item.ImageName,
                    path: item.ImagePath === null ? '' : item.ImagePath,
                    src: item.ImagePath === null ? item.ImageData : item.ImagePath,
                    data: item.ImagePath === null ? item.ImageData : item.ImagePath,
                    full: undefined
                };
                //first, input to load by order 
                self.ImageList.push(image);
                //second, process to convert base 64
                helper.toDataURL(item.ImagePath === null ? item.ImageData : item.ImagePath, function (dataUrl) {
                    image.src = dataUrl;
                    image.full = dataUrl;
                });
            });
            //console.log(self.ImageList);
            self.CropperModal.$Modal.find('div.no-image-background').hide();
            self.CropperModal.setState(self.ImageList, self.AspectRatio);
            render.imageItem.call(self);
            return self;
        }
    };


    function CropperFactory(opt) {
        this.CropperModalInstance = new MyLib.CropperModal({ $croperModal: opt.$CropperModal }).init();
    }

    CropperFactory.prototype = {
        Create: function ($input, maxNumber, aspectRatio, name, isSingle, defaultIndex) {
            var self = this;
            new CropperImages({
                $ImageUpload: $input,
                CropperModalInstance: self.CropperModalInstance,
                MaxNumber: maxNumber,
                AspectRatio: aspectRatio,
                InputName: name,
                IsSingle: isSingle,
                DefaultIndex: defaultIndex
            }).init();
            return self;
        }
    };

    return {
        CropperModal: CropperModal,
        CropperImages: CropperImages,
        CropperFactory: CropperFactory
    };
})($);
