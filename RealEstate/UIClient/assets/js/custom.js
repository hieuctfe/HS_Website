(function($) {
	
	//Cache jQuery Selector
	var $window			= $(window),
		$carousel		= $('.carousel'),   			// 2. Bootstrap slider pause time
		$dropdown		= $('.nav .dropdown-toggle'),	// 3. dropdown submenu on hover in desktopand dropdown sub menu on click in mobile
		$header			= $('#header'),					// 4. Update Header Style + Scroll to Top
		$projectimg		= $('.project-images'),			// 5. Single project slider click + Show large image in top box
		$our_team		= $('.our_team_member'),		// 8. Our Team Member Carousel Uniland
		$our_member		= $('.member-widget-view'),		// 8. Our Team Member Carousel Uniland
		$property_Grid	= $('.property_slide'),			// 8. Property Grid Slider
		$client			= $('.testimonials-carousel'),	// 9. Testimonials Carousel Slider
		$srv_slide		= $('.service-slider'),			// 10. Single service Slider
		$feedback_side	= $('.feedback_small'),			// 11. Testimonials Carousel in sidebar small
		$projectnav		= $('.project-thumbnail'),		// 12. Single Project slider carosure nav image slider
		$brand			= $('.partner-slider'),			// 13. Our Partner Logos Slider Auto
		$feedback		= $('.testimonial-part2'),		// 13. Testimonials Carousel Slider
		$contact		= $('#contact-form')			// 22. Contact Form Validation
		$landdrop		= $('.lan-drop'),			    // 23. Top Header language dropdown
		$featured_side	= $('.slide_featured')
	
	// jQuery Settings Table List
		// 1. Remove Loader
		// 2. Bootstrap slider pause time
		// 3. dropdown submenu on hover in desktop and dropdown sub menu on click in mobile
		// 4. language dropdown
		// 5. Update Header Style + Scroll to Top
		// 6. Single project slider click + Show large image in top box
		// 7 Submenu Dropdown Toggle
		// 8. Revolution Slider
		// 9. Our Team Carousel Uniland
		// 10. Our Team Member Carousel for sidebar in Uniland
		// 11. Property Grid Slider Uniland
		// 12. Testimonials Carousel Slider Home Uniland
		// 13. Testimonials Carousel in sidebar small
		// 14. Sidebar Featured Slide
		// 15. Use for Accordion Box
		// 16. Fact Counter For Achivement Counting
		// 17. LightBox / Fancybox
		// 18. Sortable Masonary with Filters
		// 19. Gallery With Filters List
		// 20. Date Picker
		// 21. Scroll to a Specific Div
		// 22. Click Search Icon and Open Search Field
		// 23. Contact Form Validation
		// 24. Elements Animation
		// 25. Acive scroll top button
		// 26. When document is Scrollig, do
		// 27. When document is loading, do
		// 28. Youtube and Vimeo video popup control
		// 29. Pricing bar Filter
		// 30. Full Screen Map Height
		// 31. Slide Up Advance Search Form On Map

	
	// 1. Remove Loader
	function handlePreloader(){
		if($('.loading-page').length){
			$('.loading-page').delay(500).fadeOut(500);
			$('body').removeClass('page-load');
		}
	}
	
	// 2. Bootstrap slider pause time
	$('.carousel').carousel({
	  interval: 6000,  // Slider Pause time
	  pause: "hover"
	});
	
	// 3. dropdown submenu on hover in desktop and dropdown sub menu on click in mobile
	$('#bs-example-navbar-collapse-1').each(function() {
		$dropdown.on('click', function(){
			if($window.width() < 992){
				if($(this).parent('.dropdown').hasClass('visible')){
					$(this).parent('.dropdown').children('.dropdown-menu').first().stop(true, true).slideUp(300);
					$(this).parent('.dropdown').removeClass('visible');
				}
				else{
					$(this).parent('.dropdown').children('.dropdown-menu').stop(true, true).slideUp(300);
					$(this).parent('.dropdown').children('.dropdown-menu').stop(true, true).slideDown(300);
					$(this).parent('.dropdown').addClass('visible');
				}
			}
		});
		
		$window.on('resize', function(){
			if($window.width() > 991){
				$('.dropdown').find('.dropdown-menu').removeAttr('style');
			}
		});
	});
	
	// 4. language dropdown
	$('.top_right').each(function() {
		$landdrop.on('show.bs.lan-drop', function(e){
		  	$(this).find('.dropdown-menu').first().stop(true, true).slideDown(300);
		});
		
		$landdrop.on('hide.bs.lan-drop', function(e){
		  	$(this).find('.dropdown-menu').first().stop(true, true).slideUp(200);
		});	
	});
	
	// 5. Update Header Style + Scroll to Top
	function headerStyle() {
		if($header.length){
			var windowpos = $window.scrollTop();
			if (windowpos >= 180) {
				$header.addClass('fixed-header');
			} else {
				$header.removeClass('fixed-header');
			}
		}
	}

	// 6. Single project slider click + Show large image in top box
	$projectimg.each(function() {
		$(this).on('click','.item a',function(event) {
			event.preventDefault();
			$(this).parents('.project-images').find(".item a").removeClass('onlive');
			$(this).addClass('onlive');
			$(this).parents('.project-images').find(".slide-project img").attr("src", $(this).attr("href"));
		});
	});
	
	// 7 Submenu Dropdown Toggle
	if($('.main-menu li.dropdown ul').length){
		$('.main-menu li.dropdown').append('<div class="dropdown-btn"></div>');
		
		//Dropdown Button
		$('.main-menu li.dropdown .dropdown-btn').on('click', function() {
			$(this).prev('ul').slideToggle(500);
		});
		
		//Disable dropdown parent link
		$('.navigation li.dropdown > a').on('click', function(e) {
			e.preventDefault();
		});
	}
		
	// 8. Revolution Slider
	if($('.revolution-slider .tp-banner').length){

		jQuery('.revolution-slider .tp-banner').show().revolution({
		  
		  delay:10000,
		  startwidth:1200,
		  startheight:720,
		  hideThumbs:600,	
		  thumbWidth:80,
		  thumbHeight:50,
		  thumbAmount:5,
		  navigationType:"bullet",
		  navigationArrows:"0",
		  navigationStyle:"preview4",	
		  touchenabled:"on",
		  onHoverStop:"off",	
		  swipe_velocity: 0.7,
		  swipe_min_touches: 1,
		  swipe_max_touches: 1,
		  drag_block_vertical: false,	
		  parallax:"mouse",
		  parallaxBgFreeze:"on",
		  parallaxLevels:[7,4,3,2,5,4,3,2,1,0],
	
		  keyboardNavigation:"off",
	
		  navigationHAlign:"center",
		  navigationVAlign:"bottom",
		  navigationHOffset:0,
		  navigationVOffset:20,
	
		  soloArrowLeftHalign:"left",
		  soloArrowLeftValign:"center",
		  soloArrowLeftHOffset:20,
		  soloArrowLeftVOffset:0,
	
		  soloArrowRightHalign:"right",
		  soloArrowRightValign:"center",
		  soloArrowRightHOffset:20,
		  soloArrowRightVOffset:0,
	
		  shadow:0,
		  fullWidth:"on",
		  fullScreen:"off",
	
		  spinner:"spinner4",
	
		  stopLoop:"off",
		  stopAfterLoops:-1,
		  stopAtSlide:-1,
	
		  shuffle:"off",
	
		  autoHeight:"off",
		  forceFullWidth:"on",
	
		  hideThumbsOnMobile:"on",
		  hideNavDelayOnMobile:1500,
		  hideBulletsOnMobile:"on",
		  hideArrowsOnMobile:"on",
		  hideThumbsUnderResolution:0,
	
		  hideSliderAtLimit:0,
		  hideCaptionAtLimit:0,
		  hideAllCaptionAtLilmit:0,
		  startWithSlide:0,
		  videoJsPath:"",
		  fullScreenOffsetContainer: ""
	  });		
	}	
	
	// 9. Our Team Carousel Uniland
	if ($our_team.length) {
		$our_team.owlCarousel({
            loop: ($our_team.length > 1),
			margin:30,
			nav:true,
			autoplayHoverPause:false,
			smartSpeed: 300,
			autoplay: false,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:2
				},
				1024:{
					items:3
				},
				1200:{
					items:4
				}
			}
		});    		
	}
	
	// 10. Our Team Member Carousel for sidebar in Uniland
	if ($our_member.length) {
		$our_member.owlCarousel({
            loop: ($our_member.length > 1),
			margin:30,
			nav:true,
			autoplayHoverPause:false,
			smartSpeed: 300,
			autoplay: false,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:2
				},
				1024:{
					items:1
				},
				1200:{
					items:1
				}
			}
		});    		
	}
	
	// 11. Property Grid Slider Uniland
	if ($property_Grid.length) {
		$property_Grid.owlCarousel({
            loop: ($property_Grid.length > 1),
			margin:30,
			nav:true,
			autoplayHoverPause:false,
			smartSpeed: 300,
			autoplay: false,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:2
				},
				1024:{
					items:3
				},
				1200:{
					items:3
				}
			}
		});    		
	}		
	
	// 12. Testimonials Carousel Slider Home Uniland
	if ($client.length) {
		$client.owlCarousel({
            loop: ($client.length > 1),
			margin:30,
			nav:true,
			autoplayHoverPause:false,
			autoplay: true,
			smartSpeed: 700,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				760:{
					items:1
				},
				1024:{
					items:1
				},
				1200:{
					items:1
				}
			}
		});    		
	}
	
	// 12. Testimonials2 Carousel Slider Home Uniland
	if ($feedback.length) {
		$feedback.owlCarousel({
            loop: ($feedback.length > 1),
			margin:30,
			nav:true,
			autoplayHoverPause:false,
			autoplay: true,
			smartSpeed: 700,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				760:{
					items:2
				},
				1024:{
					items:2
				},
				1200:{
					items:2
				}
			}
		});    		
	}	
	
	// 13. Testimonials Carousel in sidebar small
	if ($feedback_side.length) {
		$feedback_side.owlCarousel({
            loop: ($feedback_side.length > 1),
			margin:30,
			nav:true,
			autoplayHoverPause:true,
			autoplay: true,
			smartSpeed: 700,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				600:{
					items:1
				},
				760:{
					items:1
				},
				1024:{
					items:1
				},
				1200:{
					items:1
				}
			}
		});    		
	}	
	
	// 14. Featured Property Side
	if ($featured_side.length) {
		$featured_side.owlCarousel({
            loop: ($featured_side.length > 1),
			margin:30,
			nav:true,
			dots: false,
			smartSpeed: 500,
			autoplayHoverPause:true,
			autoplay: true,
			navText: [ '<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>' ],
			responsive:{
				0:{
					items:1
				},
				480:{
					items:1
				},
				600:{
					items:1
				},
				800:{
					items:1
				},
				1200:{
					items:1
				}
			}
		});    		
	}
		
	// 15. Use for Accordion Box
	if($('.according_area').length){
		$('.according_title').on('click', function() {			
			if($(this).hasClass('active')){
				$(this).addClass('active');			
			}
			
			//if ($(this).next('.according_details').is(':visible')){
			//$(this).removeClass('active');
			//}
			else{
				$('.according_title').removeClass('active');
				$('.according_details').slideUp(300);
				$(this).addClass('active');
				$(this).next('.according_details').slideDown(300);	
			}
		});	
	}
	
	
	// 16. Fact Counter For Achivement Counting
	function factCounter() {
		if($('.fact-counter').length){
			$('.fact-counter .achievement.animated').each(function() {	
				var $t = $(this),
					n = $t.find(".counting").attr("data-stop"),
					r = parseInt($t.find(".counting").attr("data-speed"), 10);
					
				if (!$t.hasClass("counted")) {
					$t.addClass("counted");
					$({
						countNum: $t.find(".count-text").text()
					}).animate({
						countNum: n
					}, {
						duration: r,
						easing: "linear",
						step: function() {
							$t.find(".counting").text(Math.floor(this.countNum));
						},
						complete: function() {
							$t.find(".counting").text(this.countNum);
						}
					});
				}
				
				//set skill building height
				var size = $(this).children('.progress-bar').attr('aria-valuenow');
				$(this).children('.progress-bar').css('width', size+'%');
				
			});
		}
	}
	
	
	// 17. LightBox / Fancybox
	if($('.photo_gallery a').length) {
		$('.photo_gallery a').fancybox({
			openEffect  : 'elastic',
			closeEffect : 'elastic',
			helpers : {
				media : {}
			}
		});
	}
	
	
	// 18. Sortable Masonary with Filters
	function enableMasonry() {
		if($('.sortable-masonry').length){
	
			var winDow = $(window);
			// Needed variables
			var $container=$('.sortable-masonry .items-container');
			var $filter=$('.sortable-masonry .filter-btns');
	
			$container.isotope({
				filter:'*',
				 masonry: {
					columnWidth : 1 
				 },
				animationOptions:{
					duration:1000,
					easing:'linear'
				}
			});
			
	
			// Isotope Filter 
			$filter.find('li').on('click', function(){
				var selector = $(this).attr('data-filter');
	
				try {
					$container.isotope({ 
						filter	: selector,
						animationOptions: {
							duration: 1000,
							easing	: 'linear',
							queue	: false
						}
					});
				} catch(err) {
	
				}
				return false;
			});
	
	
			winDow.bind('resize', function(){
				var selector = $filter.find('li.active').attr('data-filter');

				$container.isotope({ 
					filter	: selector,
					animationOptions: {
						duration: 1000,
						easing	: 'linear',
						queue	: false
					}
				});
			});
	
	
			var filterItemA	= $('.sortable-masonry .filter-btns li');
	
			filterItemA.on('click', function(){
				var $this = $(this);
				if ( !$this.hasClass('active')) {
					filterItemA.removeClass('active');
					$this.addClass('active');
				}
			});
		}
	}
	
	enableMasonry();
	
	
	// 19. Gallery With Filters List
	if($('.filter-list').length){
		$('.filter-list').mixItUp({});
	}
	
	
	// 20. Date Picker
	  if($('.datepicker').length){
		$( ".datepicker" ).datepicker();
	  }
	
	
	// 21. Scroll to a Specific Div
	if($('.scroll-btn').length){
		$(".scroll-btn").on('click', function(e) {
			e.preventDefault();
			var HeaderHeight = $('.header-lower').height();
			var target = $(this).attr('data-target');
		   // animate
		   $('html, body').animate({
		   scrollTop: $(target).offset().top }, 1000);
	
		});
	}
	
	
	// 22. Click Search Icon and Open Search Field	
	var $srcicon = $('.hvr-src a'),
		$srcfield = $('#search');
	$srcicon.on('click', function(event){
		event.preventDefault();
		$srcfield.addClass('visible');
		event.stopPropagation();
	});
	
	$('.src-close').on('click', function(){
		$srcfield.removeClass('visible');
	});
	
	$srcfield.on('click', function(event){
		event.stopPropagation();
	});
	
	$window.on('click', function(e){
		$srcfield.removeClass('visible');
    });

	// 24. Elements Animation
	if($('.wow').length){
		var wow = new WOW(
		  {
			boxClass:     'wow',      // animated element css class (default is wow)
			animateClass: 'animated', // animation css class (default is animated)
			offset:       0,          // distance to the element when triggering the animation (default is 0)
			mobile:       false,       // trigger animations on mobile devices (default is true)
			live:         true       // act on asynchronously loaded content (default is true)
		  }
		);
		wow.init();
	}
	
	// 25. Acive scroll top button
	function jumptotop(){
		var $scrollsize = $window.scrollTop();
		if($scrollsize > 300){
			$('.scroll-to-top').fadeIn(1000);
		}
		else {
			$('.scroll-to-top').fadeOut(1000);
		}
	}

    // 26. When document is Scrollig, do
	
	$window.on('scroll', function() {
		jumptotop();
		headerStyle();
		factCounter();
	});
	

   // 27. When document is loading, do
	
	$window.on('load', function() {
		handlePreloader();
		enableMasonry();
	});
	
	
	// 28. Youtube and Vimeo video popup control
	jQuery(function(){
		jQuery("a.video-popup").YouTubePopUp();
		//jQuery("a.video-popup").YouTubePopUp( { autoplay: 0 } ); // Disable autoplay
	});
	
	// 30. Full Screen Map Height
	$window.on('resize', function(){
		setMapHeight();
	});
	
	function setMapHeight(){
		var $body = $('body');
		if($body.hasClass('full-page-map')) {
			$('#map').height($window.height() - $('header').height());
		}
	}
	setMapHeight();
	
	// 31. Slide Up Advance Search Form On Map
	$('.form-up-btn').each(function(){
		$(this).on('click',function(e){
			if($('#find-location').is(".open"))
			{
				$('#find-location').removeClass("open");
				setTimeout(function(){$('#map-banner').removeClass("visible")},0);
			}
			else
			{
				$('#find-location').addClass("open");
				setTimeout(function(){
					$('#map-banner').addClass("visible")
				},400);
			}
		e.preventDefault();
		});
	});
	
	// 32. Color Settings Panel
	$('.color-panel').each(function(){
		$('.on-panel').on('click', function(){
			$('.color-panel').toggleClass('open');
		});
		
		$('.color-box li').on('click', function(){
			$('.color-box li').removeClass('active');
			$(this).addClass('active');
			 var path = $(this).data('path');
			 var logo1 = $(this).data('image');
			 var logo2 = $(this).data('target');
			 $('#color-change').attr('href', path);
			 $('.nav-logo').attr('src', logo1);
			 $('.logo-bottom').attr('src', logo2);
		});
	});
	
	// 33. Star Rating Creator
	
	function ratingEnable() {

        $('#example-reversed, .select_option').barrating('show', {
            theme: 'bars-reversed',
            showSelectedRating: true,
        });
		
		$('.select_option').barrating('show', {
            theme: 'bars-reversed',
            showSelectedRating: true,
			showValues: true,
        });
    }

    function ratingDisable() {
        $('select').barrating('destroy');
    }



    ratingEnable();
	


})(window.jQuery);