import {loadPartials, user, handleMessage, handleError} from '../util.js';

export function registerPage (context) {
    loadPartials(context)
        .then(function () {
            this.partial('./templates/register.hbs');
        })
}

export function loginPage (context) {
    loadPartials(context)
        .then(function () {
            this.partial('./templates/login.hbs');
        })
}

export function logout (context) {

    user.signOut()
        .then(function () {
            localStorage.removeItem('userInfo');
            handleMessage('Bye!');
        })
        .then(function () {
            context.redirect('/home');
        }).catch(handleError);
}


export function registerPost (context) {
    let { email, password, rePassword } = context.params;

    if (email == '') {
        return;
    }

    if (password != rePassword || password.length < 6) {
        return;
    }

    user.createUserWithEmailAndPassword(email, password)
        .then((user) => {

            localStorage.setItem('userInfo', JSON.stringify({ email, uid: user.user.uid }));

            handleMessage('Succesfully registered!');

            this.redirect('/home');
        })
        .catch(handleError);
}

export function loginPost (context) {
    let { email, password } = context.params;

    if (email == '') {
        return;
    }

    if (password.length < 6) {
        return;
    }

    user.signInWithEmailAndPassword(email, password)
        .then((user) => {

            localStorage.setItem('userInfo', JSON.stringify({ email, uid: user.user.uid }));

            handleMessage('Logged in!');

            this.redirect('/home');
        })
        .catch(handleError);
}