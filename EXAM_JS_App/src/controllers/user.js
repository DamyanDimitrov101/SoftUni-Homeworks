import { addPartials, deleteUserData, errorHandler, notificationHandler } from "../util.js";
import { register, login } from "../data.js";

export async function registerPage() {
    await addPartials(this);
    this.partial('./templates/user/registerPage.hbs');
}


export async function loginPage() {
    await addPartials(this);
    this.partial('./templates/user/loginPage.hbs');
}


export async function postRegister(ctx) {
    const {email,password, rePassword} = ctx.params;
    try {
        if (email.length==0 || password.length==0 || rePassword.length==0) {
            throw new Error('All fields are required!');
        }else if(password!==rePassword){
            throw new Error('Password do not match!');
        }
        else{
            const result = await register(email,password,rePassword);
            ctx.app.userData = result;
			notificationHandler('User registration successful.');
            ctx.redirect('/home');
        }
    } catch (error) {
        errorHandler(error.message);
    }

}


export async function postLogin(ctx) {
    const {email,password} = ctx.params;

    try {
        if (email.length==0 || password.length==0) {
            throw new Error('All fields are required!');
        }
        else{
            const result = await login(email,password);
            ctx.app.userData = result;
            notificationHandler('Login successful.');

            ctx.redirect('/home');
        }
    } catch (error) {
        errorHandler(error.message);
    }

}

export async function logout(ctx) {
    deleteUserData();

    ctx.app.userData = undefined;
    notificationHandler('Logged out!');

    ctx.redirect('/login');
}