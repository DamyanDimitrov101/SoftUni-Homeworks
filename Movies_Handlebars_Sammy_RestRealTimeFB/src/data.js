import { getUserData, getUserId, objectToArray, setUserData } from './util.js'

const apiKEY = 'AIzaSyDIzVTs8hjo-cdaCGNR0gyvjbgwJzNGtCw'; /// API_KEY
const databaseURL = 'https://testapp-c8731.firebaseio.com/'; /// DB

const endpoints = {
    LOGIN: 'https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=',
    REGISTER: 'https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=',
    ARTICLES: 'movies', ///CATALOG_DATA
    ARTICLES_BY_ID: 'movies/' /// DATA_BY_ID
};


function host(url) {
    let result = databaseURL + url + '.json';
    const auth = getUserData();

    if (auth !== null) {
        result += `?auth=${auth.idToken}`;
    }

    return result;
}

async function request(url, method, body) {
    let config = {
        method,
    };

    if (body) {
        Object.assign(config, {
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });
    }



    let response = await fetch(url, config);
    let data = await response.json();

    if (data && data.hasOwnProperty('error')) {
        const message = data.error.message;
        throw new Error(message);
    }
    
    return data;
}


async function get(url) {
    return request(url, 'GET');
}

async function post(url, body) {
    return request(url, 'POST', body)
}

async function del(url) {
    return request(url, 'DELETE');
}

async function patch(url, body) {
    return request(url, 'PATCH', body);
}

export async function login(email, password) {
    let response = await post(endpoints.LOGIN + apiKEY,
        {
            email,
            password,
            returnSecureToken: true
        });

    setUserData(response);

    return response;
}

export async function register(email, password, repeatPassword) {
    let response = await post(endpoints.REGISTER + apiKEY, {
        email,
        password,
        repeatPassword,
        returnSecureToken: true
    });

    setUserData(response);

    return response;
}

export async function getAll() {
    let res = await get(host(endpoints.ARTICLES));
    return objectToArray(res);
}

export async function getById(id) {
    
    let res = await get(host(endpoints.ARTICLES_BY_ID + id));
    res._id = id;
    return res;
}


export async function createMovie(movie) {
    const data = Object.assign({_ownerId: getUserId()}, movie);

    return post(host(endpoints.ARTICLES),data);
}


export async function editMovie(id,movie) {
    
    return await patch(host(endpoints.ARTICLES_BY_ID + id),movie);  
}

export async function deleteById(id) {
    return await del(host(endpoints.ARTICLES_BY_ID + id));
}

export async function likePageById(id,movie) {
    return await patch(host(endpoints.ARTICLES_BY_ID + id),movie); 
}

export async function getByTitle(title) {
    let res = await get(host(endpoints.ARTICLES));
    
    let arr = objectToArray(res);
   
    let movie = arr.find(m=> m.title==title);
    if (movie==undefined) {
        throw new Error('Movie was not found!');
    }
    
    return movie;
}