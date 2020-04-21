import React, { Component } from 'react';
import AppActions from './../actions/AppActions';
import { get, post } from "../lib/http";


export class UserChallenge extends Component {
  static displayName = UserChallenge.name;

  constructor(props) {
    super(props);
    UserChallenge.update();
  }


  static update() {
    get('api/userchallenge', (data) => AppActions.updateStates([
      { statePath: 'users', data },
      { statePath: 'loading', data: false }
    ]));
  }

  renderUsersTable(history, users) {
      return (
      <table className='table'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th>Image</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {users.map(u =>
            <tr key={u.id}>
              <td>{u.id}</td>
              <td>{u.name}</td>
              <td>{u.email}</td>
              <td>{u.phone}</td>
              <td><img src={u.image} alt="No image" width="200px" height="200px"/></td>
              <td><button onClick={() => history.push(`/userchallengemanager/${u.id}`)}>Edit</button></td>
            </tr>
          )}
        </tbody>
        <tfoot>
          <tr>
            <td colSpan="9"></td>
            <td><button onClick={() => history.push(`/userchallengemanager/_`)}>New</button></td>
          </tr>
        </tfoot>
      </table>
    );
  }



  render() {
    const { history, loading, users, user } = this.props;
   
    let contents = loading
      ? <p><em>Loading...</em></p>
      : this.renderUsersTable(history, users);


    return (
      <div>
        <div>
          <h1>User Challenge</h1>
          {contents}
        </div>
      </div>

    );
  }

}

UserChallenge.defaultProps = {
  loading: false,
  users: []
}