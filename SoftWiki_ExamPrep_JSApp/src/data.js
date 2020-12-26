import { getUserData, getUserId, objectToArray, setUserData } from './util.js'

const apiKEY = 'AIzaSyCotLVexU_29EvEZsx_g8LgSeW-1v_a7Gg'; /// API_KEY
const databaseURL = 'https://softwiki-203ab.firebaseio.com/'; /// DB

const endpoints = {
    LOGIN: 'https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=',
    REGISTER: 'https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=',
    ARTICLES: 'articles', ///CATALOG_DATA
    ARTICLES_BY_ID: 'articles/' /// DATA_BY_ID
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

export async function register(email, password) {
    let response = await post(endpoints.REGISTER + apiKEY, {
        email,
        password,
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


export async function createArticle(article) {
    const data = Object.assign({_ownerId: getUserId()}, article);

    return post(host(endpoints.ARTICLES),data);
}


export async function editArticle(id,article) {
    const data = Object.assign({_ownerId: getUserId()}, article);
    
    return patch(host(endpoints.ARTICLES_BY_ID + id),data);  
}

export async function deleteById(id) {
    return del(host(endpoints.ARTICLES_BY_ID + id));
}