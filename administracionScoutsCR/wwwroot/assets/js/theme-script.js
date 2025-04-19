
function loadJS(FILE_URL, async = true) {
	let scriptEle = document.createElement("script");
  
	scriptEle.setAttribute("src", FILE_URL);
	scriptEle.setAttribute("type", "text/javascript");
	scriptEle.setAttribute("async", async);
  
	document.body.appendChild(scriptEle);
  
	// success event 
	scriptEle.addEventListener("load", () => {
	  
	});
	 // error event
	scriptEle.addEventListener("error", (ev) => {
	  
	});
  }




setTimeout(function () {
    $('.main-wrapper').append('<div class="sidebar-settings nav-toggle" id="layoutDiv">' +
        '<div class="sidebar-content sticky-sidebar-one">' +
        '<div class="sidebar-header">' +
        '<div class="sidebar-theme-title">' +
        '<h5>Theme Customizer</h5>' +
        '<p>Customize & Preview in Real Time</p>' +
        '</div>' +
        '<div class="close-sidebar-icon d-flex">' +
        '<a class="sidebar-refresh me-2" onclick="resetData()">&#10227;</a>' +
        '<a class="sidebar-close" href="#">X</a>' +
        '</div>' +
        '</div>' +
        '<div class="sidebar-body p-0">' +
        '<form id="theme_color" method="post">' +
        '<div class="theme-mode mb-0">' +
        '<div class="theme-body-main">' +
        '<div class="theme-head">' +
        '<h6>Theme Mode</h6>' +
        '<p>Enjoy Dark & Light modes.</p>' +
        '</div>' +
        '<div class="row">' +
        '<div class="col-xl-6 ere">' +
        '<div class="layout-wrap">' +
        '<div class="d-flex align-items-center">' +
        '<div class="status-toggle d-flex align-items-center me-2">' +
        '<input type="radio" name="theme-mode" id="light_mode" class="check color-check stylemode lmode" value="light_mode" checked>' +
        '<label for="light_mode" class="checktoggles">' +
        '<img src="/assets/img/theme/theme-img-01.jpg" alt="">' +
        '<span class="theme-name">Light Mode</span>' +
        '</label>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '<div class="col-xl-6 ere">' +
        '<div class="layout-wrap">' +
        '<div class="d-flex align-items-center">' +
        '<div class="status-toggle d-flex align-items-center me-2">' +
        '<input type="radio" name="theme-mode" id="dark_mode" class="check color-check stylemode" value="dark_mode">' +
        '<label for="dark_mode" class="checktoggles">' +
        '<img src="/assets/img/theme/theme-img-02.jpg" alt="">' +
        '<span class="theme-name">Dark Mode</span>' +
        '</label>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</form>' +
        // ... Aquí seguiría tu contenido restante, solo cambia cada img src=~/assets/... por src="/assets/..." igual que arriba
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>'
    );

    loadJS("/assets/js/theme-settings.js", true);

}, 1000);
