import { addPartials, deleteUserData } from "../util.js";
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
    const {email,password, rePass} = ctx.params;
    try {
        if (email.length==0 || password.length==0 || rePass.length==0) {
            throw new Error('All fields are required!');
        }else if(password!==rePass){
            throw new Error('Password do not match!');
        }
        else{
            const result = await register(email,password);
            ctx.app.userData = result;
            ctx.redirect('/home');
        }
    } catch (error) {
        console.error(error);
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
            ctx.redirect('/home');
        }
    } catch (error) {
        console.error(error);
    }
}


export async function logout(ctx) {
    deleteUserData();

    ctx.app.userData = undefined;
    ctx.redirect('/home');
}