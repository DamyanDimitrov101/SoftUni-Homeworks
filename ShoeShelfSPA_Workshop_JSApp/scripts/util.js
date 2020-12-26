export const user = firebase.auth();
export const shoes = firebase.firestore().collection('shoes');

export function loadPartials(context) {
    let { email, uid } = getUserData();

    context.isLogged = Boolean(uid != undefined);

    context.email = email;

    return context.loadPartials({
        'navigation': '../templates/partials/header.hbs',
        'footer': '../templates/partials/footer.hbs'
    });
}

export function handleMessage(message) {
    let infoDiv = document.getElementById('info');

    infoDiv.style.display = 'block';
    infoDiv.textContent = message;

    setInterval(() => {
        infoDiv.style.display = 'none';
    }, 3000);
}

export function handleError(e) {
    console.log(e);
}



export function getUserData() {
    return JSON.parse(localStorage.getItem('userInfo')) || { email: undefined, uid: undefined };
}


export function getOffer(id) {
    return shoes.doc(id);
}
