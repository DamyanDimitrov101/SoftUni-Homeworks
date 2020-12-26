import { addPartials, errorHandler, getUserId, mapCategories, notificationHandler } from "../util.js";
import { createMovie, deleteById, editMovie, getAll, getById, likePageById, getByTitle } from '../data.js';

export async function homePage() {
    await addPartials(this);

    this.partials.movie = await this.load('/templates/catalog/movie.hbs'); /// Additional Partial Of Item

    const data = mapCategories(await getAll());

    const context = {};
    context.movies = data;
    context.user = await this.app.userData;
    this.partial('./templates/catalog/homePage.hbs', context);
}

export async function createPage(ctx) {
    await addPartials(this);

    const context = {
        user: this.app.userData
    };

    this.partial('./templates/catalog/createMovie.hbs', context);
}

export async function postCreate(ctx) {
    const { title, imageUrl, description } = ctx.params; /// PARAMS

    try {
        if (title.length == 0 || imageUrl.length == 0 || description.length == 0) {
            throw new Error('All fields are required!');
        } else {
            const context = await createMovie({
                title,
                imageUrl,
                description,
                likes:[]
            });

            ctx.redirect('/home');
        }
    } catch (error) {
        errorHandler(error.message);
    }
}

export async function detailsPage() {
    await addPartials(this);
    const movie = await getById(this.params._id);

    const context = {
        user: this.app.userData,
        movie: movie,
        canEdit: movie._ownerId == getUserId(),
        likesCount: Object.keys(movie.likes).length
    };
    this.partial('/templates/catalog/detailsPage.hbs', context);
}

export async function editPage() {
    await addPartials(this);
    const movie = await getById(this.params._id);
    if (movie._ownerId !== getUserId()) {
        this.redirect('/home');
    } else {
        const context = {
            user: this.app.userData,
            movie
        };

        this.partial('/templates/catalog/editPage.hbs', context);
    }
}

// POST

export async function postEdit(ctx) {
    const { title, imageUrl, description } = ctx.params; /// PARAMS

    try {
        if (title.length == 0 || imageUrl.length == 0 || description.length == 0) {
            throw new Error('All fields are required!');
        } else {
            const context = await editMovie(ctx.params._id, {
                title,
                imageUrl,
                description
            });

            ctx.redirect('/home');
        }
    } catch (error) {
        errorHandler(error.message);
    }
}

export async function deleteArticle() {
    try {
        const id = this.params._id;
        const res = await deleteById(id);
        this.redirect('/home');
    } catch (error) {
        errorHandler(error.message);
    }
}

export async function likePage() {
    try {
        const id = this.params._id;
        const uid = await getUserId();

        const movie = await getById(this.params._id);
        
        if (Array
                .from(Object.keys(movie.likes))
                .includes(uid)) {
                
                    throw new Error('Already liked!');
        }

        movie.likes[uid] = uid;

        const res = await likePageById(id,movie);
        notificationHandler('Liked!');
        this.redirect(`/details/${id}`);
    } catch (error) {
        errorHandler(error.message);
    }
}

export async function searchPage() {
   
    try {
        const movie = await getByTitle(this.params.title);

        this.redirect(`/details/${movie._id}`);
    } catch (error) {
        errorHandler(error.message);
    }
}