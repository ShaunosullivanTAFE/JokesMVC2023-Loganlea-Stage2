﻿
async function switchTheme() {
    // retrieve the currently stored theme from localstorage (this may have a value, or be undefined)
    let currentTheme = localStorage.getItem('theme')

    // if the current theme is not 'undefined' and is set to dark, then we are setting the theme to 'light'
    if (currentTheme && currentTheme == 'light') {

        // update the localStorage to set the theme to light
        localStorage.setItem('theme', 'dark')

        // Send a POST request to the server to update the session to record the change in theme
        let result = await fetch('/api/Settings/SetTheme', {
            method: 'POST',
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({ theme: "dark" })
        })
        console.log(result)
        // perform the client-side change to the theme, by switching out the stylesheet (this is faster with caching enabled!)
        document.getElementById('themeStyle').setAttribute('href', '/css/DarkTheme.css')

    } else {
        // setting the theme to dark
        localStorage.setItem('theme', 'light')

        // sending a POST request with a query parameter - simpler than sending a post request with Body parameters
        let result = await fetch('/api/Settings/SetTheme', {
            method: 'POST',
            headers: {
                "content-type": "application/json"
            },
            body: JSON.stringify({theme: "light"})
        })
        console.log(result)

        document.getElementById('themeStyle').setAttribute('href', '/css/LightTheme.css')
    }
}

function ShowToast(text, duration, type){

    let backgroundColour;

    switch(type){
        case 'error':
            backgroundColour = "linear-gradient(to right, #EC4B61, #B40D24)";
            break;
        case 'success':
            backgroundColour = "linear-gradient(to right, #3FC63F, #0B980B)";
            break;
        case 'default':
            backgroundColour = "linear-gradient(to right, #7D68C1, #301782)";
            break;
    }


    Toastify({
        text: text,
        duration: duration,
        newWindow: true,
        close: false,
        gravity: "top",
        position: "left",
        stopOnFocus: true,
        style: {
            background: backgroundColour
        },
        onclick: function() {}
    }).showToast();

}