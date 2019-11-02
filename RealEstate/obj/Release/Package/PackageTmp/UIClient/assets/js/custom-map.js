var mapStyles = [{featureType:'water',elementType:'all',stylers:[{hue:'#d7ebef'},{saturation:-5},{lightness:54},{visibility:'on'}]},{featureType:'landscape',elementType:'all',stylers:[{hue:'#eceae6'},{saturation:-49},{lightness:22},{visibility:'on'}]},{featureType:'poi.park',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-81},{lightness:34},{visibility:'on'}]},{featureType:'poi.medical',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-80},{lightness:-2},{visibility:'on'}]},{featureType:'poi.school',elementType:'all',stylers:[{hue:'#c8c6c3'},{saturation:-91},{lightness:-7},{visibility:'on'}]},{featureType:'landscape.natural',elementType:'all',stylers:[{hue:'#c8c6c3'},{saturation:-71},{lightness:-18},{visibility:'on'}]},{featureType:'road.highway',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-92},{lightness:60},{visibility:'on'}]},{featureType:'poi',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-81},{lightness:34},{visibility:'on'}]},{featureType:'road.arterial',elementType:'all',stylers:[{hue:'#dddbd7'},{saturation:-92},{lightness:37},{visibility:'on'}]},{featureType:'transit',elementType:'geometry',stylers:[{hue:'#c8c6c3'},{saturation:4},{lightness:10},{visibility:'on'}]}];

$.ajaxSetup({
    cache: true
});

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Google Map - Homepage
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function initMap(){
		var mapOptions = {
			zoom: 14,
			center: new google.maps.LatLng(48.874796, 2.299275), // map position of view first latitude, and longitude
			styles: [{"featureType":"water","elementType":"all","stylers":[{"hue":"#76aee3"},{"saturation":38},{"lightness":-11},{"visibility":"on"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"hue":"#8dc749"},{"saturation":-47},{"lightness":-17},{"visibility":"on"}]},{"featureType":"poi.park","elementType":"all","stylers":[{"hue":"#c6e3a4"},{"saturation":17},{"lightness":-2},{"visibility":"on"}]},{"featureType":"road.arterial","elementType":"all","stylers":[{"hue":"#cccccc"},{"saturation":-100},{"lightness":13},{"visibility":"on"}]},{"featureType":"administrative.land_parcel","elementType":"all","stylers":[{"hue":"#5f5855"},{"saturation":6},{"lightness":-31},{"visibility":"on"}]},{"featureType":"road.local","elementType":"all","stylers":[{"hue":"#ffffff"},{"saturation":-100},{"lightness":100},{"visibility":"simplified"}]},{"featureType":"water","elementType":"all","stylers":[]}]
		};
		var bounds = new google.maps.LatLngBounds();
						
		// Display a map on the web page
		var mapElement = document.getElementById('map');
		var map = new google.maps.Map(mapElement, mapOptions);		
		map.setTilt(50);
		var locations = [
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.87, 2.29, "property-detail.html", "assets/img/properties/property-01.jpg", "assets/img/property-types/apartment.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.866876, 2.309639, "property-detail.html", "assets/img/properties/property-02.jpg", "assets/img/property-types/apartment.png"],

			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.874796, 2.299275, "property-detail.html", "assets/img/properties/property-03.jpg", "assets/img/property-types/construction-site.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.864194, 2.288868, "property-detail.html", "assets/img/properties/property-04.jpg", "assets/img/property-types/cottage.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.881187, 2.276938, "property-detail.html", "assets/img/properties/property-06.jpg", "assets/img/property-types/garage.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.859098, 2.423515, "property-detail.html", "assets/img/properties/property-08.jpg", "assets/img/property-types/houseboat.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.872296, 2.287796, "property-detail.html", "assets/img/properties/property-07.jpg", "assets/img/property-types/land.png"],

			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.874457, 2.254386, "property-detail.html", "assets/img/properties/property-09.jpg", "assets/img/property-types/single-family.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.875191, 2.252412, "property-detail.html", "assets/img/properties/property-10.jpg", "assets/img/property-types/villa.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.864352, 2.257218, "property-detail.html", "assets/img/properties/property-11.jpg", "assets/img/property-types/vineyard.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.858648, 2.273526, "property-detail.html", "assets/img/properties/property-12.jpg", "assets/img/property-types/warehouse.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.856277, 2.264256, "property-detail.html", "assets/img/properties/property-13.jpg", "assets/img/property-types/industrial-site.png"],

			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.859988, 2.252991, "property-detail.html", "assets/img/properties/property-01.jpg", "assets/img/property-types/apartment.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.856954, 2.283912, "property-detail.html", "assets/img/properties/property-05.jpg", "assets/img/property-types/condominium.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.879268, 2.270672, "property-detail.html", "assets/img/properties/property-06.jpg", "assets/img/property-types/construction-site.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.875925, 2.3239098, "property-detail.html", "assets/img/properties/property-03.jpg", "assets/img/property-types/cottage.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.870393, 2.327771, "property-detail.html", "assets/img/properties/property-04.jpg", "assets/img/property-types/houseboat.png"],

			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.880328, 2.307258, "property-detail.html", "assets/img/properties/property-08.jpg", "assets/img/property-types/land.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.880284, 2.306721, "property-detail.html", "assets/img/properties/property-09.jpg", "assets/img/property-types/single-family.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.860342, 2.304597, "property-detail.html", "assets/img/properties/property-10.jpg", "assets/img/property-types/vineyard.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.852549, 2.329574, "property-detail.html", "assets/img/properties/property-11.jpg", "assets/img/property-types/warehouse.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.857124, 2.316699, "property-detail.html", "assets/img/properties/property-12.jpg", "assets/img/property-types/empty.png"],

			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.869433, 2.315068, "property-detail.html", "assets/img/properties/property-13.jpg", "assets/img/property-types/apartment.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.885916, 2.297130, "property-detail.html", "assets/img/properties/property-01.jpg", "assets/img/property-types/industrial-site.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.893534, 2.276616, "property-detail.html", "assets/img/properties/property-02.jpg", "assets/img/property-types/construction-site.png"],
			['3398 Lodgeville Road', "Golden Valley, MN 55427", "$28,000", 48.872570, 2.237349, "property-detail.html", "assets/img/properties/property-03.jpg", "assets/img/property-types/cottage.png"],
			['2479 Murphy Court', "Minneapolis, MN 55402", "$36,000", 48.879344, 2.226191, "property-detail.html", "assets/img/properties/property-04.jpg", "assets/img/property-types/garage.png"],

		];
		var i;
        var newMarkers = [];
		for (i = 0; i < locations.length; i++) {
			
			var pictureLabel = document.createElement("img");
			pictureLabel.src = locations[i][7];
			var boxText = document.createElement("div");
			infoboxOptions = {
				content: boxText,
				disableAutoPan: false,
				//maxWidth: 150,
				pixelOffset: new google.maps.Size(-100, 0),
				zIndex: null,
				alignBottom: true,
				boxClass: "infobox-wrapper",
				enableEventPropagation: true,
				closeBoxMargin: "0px 0px -8px 0px",
				closeBoxURL: "assets/img/close-btn.png",
				infoBoxClearance: new google.maps.Size(1, 1)
			};

			marker = new google.maps.Marker({
				position: new google.maps.LatLng(locations[i][3], locations[i][4]), // loop location parameater,
				map: map,
				icon: 'http://unicoderbd.com/theme/html/uniland/img/marker_blue.png',
				title: locations[i][0]
			});
			
			newMarkers.push(marker);
			boxText.innerHTML =
				'<div class="infobox-inner">' +
					'<a href="' + locations[i][5] + '">' +
					'<div class="infobox-image" style="position: relative">' +
					'<img src="' + locations[i][6] + '">' + '<div><span class="infobox-price">' + locations[i][2] + '</span></div>' +
					'</div>' +
					'</a>' +
					'<div class="infobox-description">' +
					'<div class="infobox-title"><a href="'+ locations[i][5] +'">' + locations[i][0] + '</a></div>' +
					'<div class="infobox-location">' + locations[i][1] + '</div>' +
					'</div>' +
					'</div>';
			//Define the infobox
			newMarkers[i].infobox = new InfoBox(infoboxOptions);
			google.maps.event.addListener(marker, 'click', (function(marker, i) {
				return function() {
					for (h = 0; h < newMarkers.length; h++) {
						newMarkers[h].infobox.close();
					}
					newMarkers[i].infobox.open(map, this);
				}
			})(marker, i));
			



		}
}
// Load initialize function
		google.maps.event.addDomListener(window, 'load', initMap);



	