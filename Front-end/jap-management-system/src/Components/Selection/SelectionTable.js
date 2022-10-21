import React from 'react';
import { Link } from 'react-router-dom';
import { Button, Form } from 'react-bootstrap';

import searchIcon from '../../Assets/searchIcon.png';
import sortIconAsc from '../../Assets/sortIconDesc.png';
import sortIconDesc from '../../Assets/sortIconAsc.png';

import classes from '../Students/StudentTable.module.css';

const SelectionTable = ({
  handlePageState,
  handleSortAction,
  handleSearchState,
  handleEditState,
  handleDeleteState,
  handleResetFilters,
  selections,
  paginationInfo,
  sortState,
  searchState
}) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        <caption>List of selections</caption>
        <caption className={classes.pageButtons}>
          <br />
          {paginationInfo.currentPage > 1 && (
            <React.Fragment>
              <Button name="prev" variant="primary" onClick={handlePageState}>
                &larr;&nbsp; Previous Page
              </Button>
              <span>&nbsp; &nbsp; &nbsp;</span>
            </React.Fragment>
          )}

          {paginationInfo.currentPage < paginationInfo.pageCount && (
            <Button name="next" variant="primary" onClick={handlePageState}>
              Next Page &nbsp;&rarr;
            </Button>
          )}
          <div>
            <br />
            <span>{`Page ${paginationInfo.currentPage} of ${paginationInfo.pageCount}`}</span>
          </div>
        </caption>
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="name"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'name' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'name' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Name:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="dateStart"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'dateStart' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'dateStart' &&
                          !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Date Start:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="dateEnd"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'dateEnd' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'dateEnd' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Date End:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="status"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'status' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'status' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Status:</span>
              </div>
            </th>
            <th scope="col">
              <div className={classes.column__Title__Sort}>
                <div
                  name="program"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'program' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'program' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Program:</span>
              </div>
            </th>

            <th scope="col">
              <div className={classes.column__Title__Sort}>Actions: </div>
            </th>
          </tr>
          <tr>
            <th scope="col">
              <img
                className={classes.searchIcon}
                src={searchIcon}
                alt="search"
              />
            </th>
            <th scope="col">
              <input
                type="text"
                name="name"
                className="form-control"
                placeholder="Name:"
                value={searchState.name}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="dateStart"
                className="form-control"
                type="date"
                placeholder="Date Start:"
                value={searchState.dateStart}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <input
                name="dateEnd"
                className="form-control"
                type="date"
                placeholder="Date End:"
                value={searchState.dateEnd}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <Form.Select
                name="status"
                className="form-select"
                defaultValue=""
                aria-label="Default select example"
                value={searchState.status}
                onChange={handleSearchState}
              >
                <option value="">Select Status</option>
                <option value="Active">Active</option>
                <option value="Completed">Completed</option>
              </Form.Select>
            </th>
            <th scope="col">
              <input
                name="japProgramName"
                className="form-control"
                type="text"
                placeholder="Program:"
                value={searchState.japProgramName}
                onChange={handleSearchState}
              />
            </th>
            <th scope="col">
              <Button
                style={{ maxWidth: 120 }}
                name="resetFilters"
                className="form-control"
                type="text"
                onClick={handleResetFilters}
              >
                Reset Filters
              </Button>
            </th>
          </tr>
        </thead>
        <tbody>
          {selections &&
            selections.map((s, index) => {
              return (
                <tr key={s.id}>
                  <th scope="row">{index + 1}</th>
                  <td>{s.name}</td>
                  <td>{s.dateStart.split('T')[0]}</td>
                  <td>{s.dateEnd.split('T')[0]}</td>
                  <td>{s.status}</td>
                  <td>
                    {!s.japProgram ? <b>Not allocated</b> : s.japProgram.name}
                  </td>
                  <td>
                    <Link to={`/?selection=${s.name}`}>
                      <Button
                        style={{ minWidth: 170 }}
                        className={classes.action__button}
                        variant="primary"
                      >
                        Modify Students
                      </Button>
                    </Link>
                    <div>
                      <Button
                        className={classes.action__button}
                        variant="success"
                        onClick={handleEditState}
                      >
                        <input id={s.id} type="hidden" />
                        Edit
                      </Button>
                      <Button
                        className={classes.action__button}
                        variant="danger"
                        onClick={handleDeleteState}
                      >
                        <input id={s.id} type="hidden" />
                        Delete
                      </Button>
                    </div>
                  </td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default SelectionTable;
