// Retrieve the selected radio button value from localStorage, if available
// Theme Color
const themeColor = localStorage.getItem('selectedColorValue');
const themeColorHtmlElement = document.getElementsByTagName('html')[0];
themeColorHtmlElement.setAttribute("data-layout-mode", "light_mode");
    
if (themeColor) {
   const themeColorElement = document.querySelector(`input[value="${themeColor}"]`);


   const themeColorHtmlElement = document.getElementsByTagName('html')[0];

   themeColorHtmlElement.setAttribute("data-layout-mode", themeColor);
   if (themeColorElement) {
      themeColorElement.checked = true;
   }
}
// Add an event listener to the radio buttons to store the selected value in localStorage
const colorButtons = document.querySelectorAll('input.color-check');

colorButtons.forEach(color => {
   color.addEventListener('change', (event) => {
      const selectedColor = event.target.value;
      localStorage.setItem('selectedColorValue', selectedColor);
      if (selectedColor) {
         // Find the HTML element by its id
         const themeColorHtmlElement = document.getElementsByTagName('html')[0];
         
         themeColorHtmlElement.setAttribute("data-layout-mode", selectedColor);
      }
   });
});




// Retrieve the selected radio button value from localStorage, if available
// Data Layout
const dataLayout = localStorage.getItem('selectedLayoutValues');
const dataLayoutHtmlElement = document.getElementsByTagName('html')[0];
dataLayoutHtmlElement.setAttribute("data-layout-style", "default");
    
if (dataLayout) {
   const dataLayoutElement = document.querySelector(`input[value="${dataLayout}"]`);


   const dataLayoutHtmlElement = document.getElementsByTagName('html')[0];

   dataLayoutHtmlElement.setAttribute("data-layout-style", dataLayout);
   if (dataLayoutElement) {
      dataLayoutElement.checked = true;
   }
}
// Add an event listener to the radio buttons to store the selected value in localStorage
const layoutButtons = document.querySelectorAll('input.layout-mode');

layoutButtons.forEach(layout => {
   layout.addEventListener('change', (event) => {
      const selectedValues = event.target.value;
      localStorage.setItem('selectedLayoutValues', selectedValues);
      if (selectedValues) {
         // Find the HTML element by its id
         const themeColorHtmlElement = document.getElementsByTagName('html')[0];
         
         themeColorHtmlElement.setAttribute("data-layout-style", selectedValues);
      }
   });
});

// Retrieve the selected radio button value from localStorage, if available
// Nav Color
const navigationColor = localStorage.getItem('selectedNavcolorValue');
const navigationColorHtmlElement = document.getElementsByTagName('html')[0];
navigationColorHtmlElement.setAttribute("data-nav-color", "light");
    
if (navigationColor) {
   const navigationColorElement = document.querySelector(`input[value="${navigationColor}"]`);


   const navigationColorHtmlElement = document.getElementsByTagName('html')[0];

   navigationColorHtmlElement.setAttribute("data-nav-color", navigationColor);
   if (navigationColorElement) {
      navigationColorElement.checked = true;
   }
}
// Add an event listener to the radio buttons to store the selected value in localStorage
const navcolorButtons = document.querySelectorAll('input.nav-color');

navcolorButtons.forEach(navcolor => {
   navcolor.addEventListener('change', (event) => {
      const selectedNavcolor = event.target.value;
      localStorage.setItem('selectedNavcolorValue', selectedNavcolor);
      if (selectedNavcolor) {
         // Find the HTML element by its id
         const navigationColorHtmlElement = document.getElementsByTagName('html')[0];
        
         navigationColorHtmlElement.setAttribute("data-nav-color", selectedNavcolor);
      }
   });
});


let enableFirstFunction = true;
function toggleClass() {
   //alert(enableFirstFunction);
   var mainLayout = document.getElementById("layoutDiv");

   if (enableFirstFunction) {
       mainLayout.classList.add("show-settings");
       
    document.body.style.overflow = 'auto'; // Show the scrollbar
      } else {
         enableFirstFunction = !enableFirstFunction;
       mainLayout.classList.remove("show-settings");
       
    document.body.style.overflow = 'auto'; // Show the scrollbar
   }
  
}

var darkModeLayout = document.getElementById("dark_mode");
var lightModeLayout = document.getElementById("light_mode");
var boxModeLayout = document.getElementById("box_layout");
if (darkModeLayout) {
   darkModeLayout.addEventListener("click", toggleClass);
} else if (lightModeLayout) {
   lightModeLayout.addEventListener("click", toggleClass);
}





function resetData()
{
   document.getElementById("light_mode").checked = true;
   document.getElementById("default_layout").checked = true;
    document.getElementById("light_color").checked = true;
    document.body.style.overflow = 'auto'; // Show the scrollbar
   const htmlElment = document.getElementsByTagName('html')[0];
   localStorage.setItem('selectedLayoutValues', 'default');
   localStorage.setItem('selectedColorValue', 'light_mode');
   localStorage.setItem('selectedNavcolorValue', 'light');
   htmlElment.setAttribute("data-layout-mode", "light_mode");
   htmlElment.setAttribute("data-layout-style", "default");
   htmlElment.setAttribute("data-nav-color", "light");
   return false;
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