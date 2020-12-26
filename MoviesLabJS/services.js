const baseURL= 'https://testapp-c8731.firebaseio.com/';

const api_KEY ='AIzaSyDIzVTs8hjo-cdaCGNR0gyvjbgwJzNGtCw';
const pathLogin =  `https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${api_KEY}`;

const pathRegister =  `https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=${api_KEY}`;



const authService = {
    async login(email, password) {
        let config = {
            method:"POST",
            headers: {'content-type': 'application/json'},
            body: JSON.stringify({email,password})
        }

        let res = await fetch(pathLogin,config);
        let data = res.json();

        return data;
    },

    async register(email,password,repeatPassword){
        if (email=='') {
            throw new Error('Email must not be empty!')
        }

        if (password!==repeatPassword) {
            throw new Error('Password do not match!')            
        }
        let config = {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body:JSON.stringify({email,password})
        }

        let res = await fetch(pathRegister,config);
        let data = res.json();

        return data;
    },

    getData(){
        let data = JSON.parse(localStorage.getItem('userInfo'));
        return data;
    },

    logout(){
        localStorage.setItem('userInfo',JSON.stringify({email:'',registered:false}));
    }


}

const movieService= {
    async getAll(){
        let res = await fetch(baseURL+'movies.json');
        
        let data = res.json();

        return data;
    },

    async getOne(id){
        let res = await fetch(baseURL+`movies/${id}.json`);

        let data =await res.json();

        return data;
    }
}