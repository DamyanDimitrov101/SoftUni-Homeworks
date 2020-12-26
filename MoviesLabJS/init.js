const elements = {
    nav: () => document.querySelector('.navigation'),
    addAndSearchMovieSection: () => document.getElementById('addAndSearchMovie'),

}

async function regPartials() {
    let navigation = Handlebars.compile(document.getElementById('nav-template').innerHTML);
    let footer = Handlebars.compile(document.getElementById('footer-template').innerHTML);
    let myMovies = Handlebars.compile(document.getElementById('myMovies-template').innerHTML);

    Handlebars.registerPartial('navigation', navigation);
    Handlebars.registerPartial('footer', footer);
    Handlebars.registerPartial('myMovies', myMovies);
}

async function initialize() {

    regPartials();
    await navigate('home');
    await addEventListeners();

}

initialize();

async function addEventListeners() {
    elements.nav().addEventListener('click', navigationHandler);
    
    if (authService.getData().email!=undefined && location.pathname=='/home') {        
        await elements.addAndSearchMovieSection().addEventListener('click', navigationHandler);
    }
}

async function navigationHandler(e) {    
    await e.preventDefault();

    if (e.target.tagName !== 'A') {
        return;
    }


    let url = new URL(e.target.href);


    await navigate(url.pathname.slice(1));
    await addEventListeners();
}


function onLoginSubmit(e) {
    e.preventDefault()

    let form = new FormData(document.forms['loginForm']);

    let email = form.get('email');
    let password = form.get('password');

    authService.login(email, password)
        .then(data => {

            if (data.error != undefined) {
                handleError(data.error.message);
            } else {
                handleNotification('Welcome!');
                localStorage.setItem('userInfo', JSON.stringify({ email: data.email, uid: data.localId, registered: data.registered }));

                navigate('home');
            }
        });
}

function onRegisterSubmit(e) {
    e.preventDefault();

    let form = new FormData(document.forms['registerForm']);

    let email = form.get('email');
    let password = form.get('password');
    let repeatPassword = form.get('repeatPassword');

    authService.register(email, password, repeatPassword)
        .then(data => {
            if (data.error != undefined) {
                handleError(data.error.message);
            } else {
                handleNotification('Successfully sign up!');
                localStorage.setItem('userInfo', JSON.stringify({ email: data.email, uid: data.localId, registered: Boolean(data.localId) }));
            }
        })
        .then(data => {
            navigate('home');
        });
}

function onAddMovieSubmit(e) {
    e.preventDefault();

    let form = new FormData(document.forms['addMovieForm']);

    let title = form.get('title');
    let description = form.get('description');
    let imageUrl = form.get('imageUrl');


}


function handleNotification(message) {
    let box = document.getElementById('successB');

    box.style.display = 'block';
    box.querySelector('p').innerHTML = message;

    setTimeout(() => { box.style.display = 'none' }, 3000)
}

function handleError(message) {
    let box = document.getElementById('errorB');

    box.style.display = 'block';
    box.querySelector('p').innerHTML = message;

    setTimeout(() => { box.style.display = 'none' }, 3000)
}


