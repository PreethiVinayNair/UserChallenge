import alt from './../alt';
import AppActions from './../actions/AppActions';
import setValueByPath from './../lib/setValueByPath';
import selectn from 'selectn';
import Path from './../lib/Path';
import _ from 'lodash';

const reservedStateKeys = ['history', 'location', 'params', 'route', 'routeParams', 'routes', 'children'];

class AppStore {
  constructor() {
    this.reset();

    this.bindListeners({
      handleUpdateState: AppActions.UPDATE_STATE,
      handleUpdateStates: AppActions.UPDATE_STATES
    });
  }

  persistedData() {
    return JSON.parse(sessionStorage.persist || '{}');
  }

  persistState(statePath) {
    const path = new Path(statePath);
    const statePathString = path.toString();
    const data = selectn(statePathString, this);
    const persisted = this.persistedData();
    const key = path.route[0];
    setValueByPath(persisted, key, data);
    sessionStorage.persist = JSON.stringify(persisted, null, 2);
  }

  reset() {
    for (let key of Object.keys(this)) {
      if (!_.includes(reservedStateKeys, key)) {
        delete this[key];
      }
    }

    const persisted = this.persistedData();

    for (let key of Object.keys(persisted)) {
      setValueByPath(this, key, persisted[key]);
    }
  }

  handleUpdateState({ statePath, data, part, persist }) {
    if (statePath) {
      setValueByPath(this, statePath, data, part);

      if (persist) {
        this.persistState(statePath);
      }
    }
  }

  handleUpdateStates(states) {
    _.each(states, state => {
      setValueByPath(this, state.statePath, state.data, state.part);

      if (state.persist) {
        this.persistState(state.statePath);
      }
    });
  }
}

export default alt.createStore(AppStore, 'AppStore');