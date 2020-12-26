import { addPartials, getUserId, mapCategories, categoryMap } from "../util.js";
import { createArticle, deleteById, editArticle, getAll, getById } from '../data.js';

export async function homePage() {
    if (this.app.userData == undefined) {
        this.redirect('/login');
        return;
    }

    await addPartials(this);

    this.partials.article = await this.load('/templates/catalog/article.hbs'); /// Additional Partial Of Item

    const data = mapCategories(await getAll()); // Set In Categories

    Object.entries(data).forEach(([k, v]) => {
        v.sort((a, b) => {return a.title.localeCompare(b.title) })    
    });

    const context = data;
    context.user = this.app.userData;

    this.partial('./templates/catalog/homePage.hbs', context);
}

export async function createPage(ctx) {
    await addPartials(this);

    const context = {
        user: this.app.userData
    };

    this.partial('./templates/catalog/createPage.hbs', context);
}

export async function postCreate(ctx) {
    const { title, category, content } = ctx.params; /// PARAMS

    try {
        if (title.length == 0 || category.length == 0 || content.length == 0) {
            throw new Error('All fields are required!');
        } else if (categoryMap.hasOwnProperty(category) === false) {
            throw new Error('Invalid category!');
        } else {
            const context = await createArticle({
                title,
                category,
                content
            });

            ctx.redirect('/home');
        }
    } catch (error) {
        console.error(error.message);
    }
}


export async function detailsPage() {
    await addPartials(this);

    const article = await getById(this.params.id);

    const context = {
        user: this.app.userData,
        article,
        canEdit: article._ownerId == getUserId()
    };
    this.partial('/templates/catalog/detailsPage.hbs', context);
}

export async function editPage() {
    await addPartials(this);

    const article = await getById(this.params.id);

    if (article._ownerId !== getUserId()) {
        this.redirect('/home');
    } else {
        const context = {
            user: this.app.userData,
            article
        };

        this.partial('/templates/catalog/editPage.hbs', context);
    }
}

// POST

export async function postEdit(ctx) {
    const { title, category, content } = ctx.params; /// PARAMS

    try {
        if (title.length == 0 || category.length == 0 || content.length == 0) {
            throw new Error('All fields are required!');
        } else if (categoryMap.hasOwnProperty(category) === false) {
            throw new Error('Invalid category!');
        } else {
            const context = await editArticle(ctx.params.id, {
                title,
                category,
                content
            });

            ctx.redirect('/home');
        }
    } catch (error) {
        console.error(error.message);
    }
}

export async function deleteArticle() {
    try {
        const id = this.params.id;
        const res = await deleteById(id);

        this.redirect('/home');
    } catch (error) {
        console.error(error.message);
    }
}