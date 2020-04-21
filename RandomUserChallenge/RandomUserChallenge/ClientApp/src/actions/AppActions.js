import alt from './../alt';

class AppActions {
    updateState(statePath, data, part, persist) {
        return {
            statePath,
            data,
            part,
            persist
        };
    }

    updateStates(stateArray) {
        return stateArray;
    }
}

export default alt.createActions(AppActions);