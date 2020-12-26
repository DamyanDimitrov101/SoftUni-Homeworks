import {  createPage, deleteArticle, detailsPage, editPage, homePage, postCreate, postEdit, likePage, searchPage } from "./controllers/catalog.js";
import {  loginPage, postLogin, postRegister, registerPage, logout } from "./controllers/user.js";
import { getUserData } from "./util.js";

const app = Sammy('#root', function (context) {
    
    this.use('Handlebars', 'hbs');

    const user = getUserData();
    context.userData = user;

    this.get('/', homePage);
    this.get('/home', homePage);



    /// USER
    this.get('/login', loginPage);
    this.get('/register', registerPage);
    this.get('/logout', logout);
    

    this.post('/login',(ctx) =>  {postLogin(ctx); });
    this.post('/register',(ctx) =>  {postRegister(ctx); });
    ///
    

    this.get('/create', createPage);
    this.post('/create',(ctx) =>  {postCreate(ctx); });
    
    this.get('/details/:_id', detailsPage);
    this.get('/delete/:_id', deleteArticle);
    
    
    this.get('/edit/:_id', editPage);
    this.post('/edit/:_id',(ctx) =>  {postEdit(ctx); });


    this.get('/like/:_id', likePage);

    this.get('/search', searchPage);
});


app.run();

function search(e) {
    e.preventDefault();

    console.log(e.target);
}