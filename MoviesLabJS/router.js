const routes = {
    'home': 'home-template',
    'login': 'login-template',
    'register': 'register-template',
    'addMovie': 'addMovie-template',
    'details': 'details-template',

};

const router = async (fullPath) => {
    let [path,id] = fullPath.split('/');
    let app = document.getElementById('app');
    let data = {};

   
    data = authService.getData();

    switch (path) {
        case 'home':
            await movieService.getAll()
                .then(movies => {
                    let arr = Object.keys(movies)
                        .map(m=>movies[m].key=m)
                        .map(m => movies[m]);
                    
                    data = { ...data, movies: arr };
                })
                .then(mov=>{
                    //console.log(data);
                    let template = Handlebars.compile(document.getElementById(routes[path]).innerHTML);
                    app.innerHTML = template(data);
                });

            return;
        case 'logout':
            authService.logout();
            navigate('home');
            return;

        case 'details':
                let details =await movieService.getOne(id)
                    .then(movie=> {
                        let template = Handlebars.compile(document.getElementById(routes[path]).innerHTML);

                        data = { ...data, ...movie};
                        app.innerHTML = template(movie);
                    });
                return;
        default:
            break;
    }
    let template = Handlebars.compile(document.getElementById(routes[path]).innerHTML);
    app.innerHTML = template(data);
};

async function navigate(pathName) {
    let [path,id] = pathName.split('/');

    history.pushState({}, '', path);
  
    await router(pathName);
}