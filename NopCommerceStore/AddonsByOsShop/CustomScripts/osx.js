/*
 * SimpleModal OSX Style Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: osx.js 238 2010-03-11 05:56:57Z emartin24 $
 */

jQuery(function ($) {
	var OSX = {
		container: null,
		init: function () {
			$("input.osx, a.osx").click(function (e) {
				e.preventDefault();	

				$("#osx-modal-content").modal({
					overlayId: 'osx-overlay',
					containerId: 'osx-container',
					closeHTML: null,
					minHeight: 10,
					opacity: 65, 
					position: ['50'],
					overlayClose: true,
					onOpen: OSX.open,
					onClose: OSX.close
				});
			});
		},
		open: function (d) {
			var self = this;
			self.container = d.container[0];
			d.overlay.fadeIn('slow', function () {
			    $("#osx-modal-content", self.container).fadeIn();
				var title = $("#osx-modal-title", self.container);
				title.fadeIn();
				$("div.close", self.container).fadeIn();
				$("#osx-modal-data", self.container).fadeIn();
				d.container.fadeIn();
				d.container.fadeIn();
				});

		
		},
		close: function (d) {
			var self = this; // this = SimpleModal object
			d.container.fadeOut("500");
				
		self.close(); // or $.modal.close();
			
			
		}
	};

	OSX.init();

});