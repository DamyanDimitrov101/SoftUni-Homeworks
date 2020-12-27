export function setUserData(data) {
    sessionStorage.setItem('auth', JSON.stringify(data));
};

export function getUserData() {
    let auth = sessionStorage.getItem('auth');

    if (auth != null) {
        return JSON.parse(auth);
    } else {
        return null;
    }
};
export function getUserId() {
    let auth = sessionStorage.getItem('auth');

    if (auth != null) {
        return JSON.parse(auth).localId;
    } else {
        return null;
    }
};
export function objectToArray(data) {
    if (data == null) {
        return [];
    } else {
        return Object.entries(data).map(([k, v]) => Object.assign({ _id: k }, v));
    }
};

export async function addPartials(ctx) {
    const partials = await Promise.all([
        ctx.load('/templates/common/header.hbs'),
        ctx.load('/templates/common/footer.hbs'),
    ]);

    ctx.partials = {
        header: partials[0],
        footer: partials[1]
    };
}

export function mapCategories(data) {
    const result = [];

    for (const movie of data) {
        result.push(movie);
    }

    return result;
}

export function deleteUserData() {
    sessionStorage.removeItem('auth');
};

export function notificationHandler(message) {
    let section = document.querySelector('.infoBox');
    section.style.display = 'block';
    section.firstChild.textContent = message;
    
    setInterval(() => {
        section.style.display = 'none';
    }, 3000);
}


export function errorHandler(message) {
    

    let section = document.querySelector('.errorBox');
    section.style.display = 'block';
    section.firstChild.textContent = message;
    
    setInterval(() => {
        section.style.display = 'none';
    }, 3000);
}