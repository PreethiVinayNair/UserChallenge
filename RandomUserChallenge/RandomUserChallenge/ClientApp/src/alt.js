import Alt from 'alt';
import selectn from 'selectn';

const alt = new Alt();

window.getState = (path) => {
    const state = alt.stores.AppStore.state;

    return selectn(path, state);
};

export default alt;