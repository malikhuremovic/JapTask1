import React from 'react';
import { Button } from 'react-bootstrap';

import { Link } from 'react-router-dom';

import searchIcon from '../../Assets/searchIcon.png';
import sortIconAsc from '../../Assets/sortIconDesc.png';
import sortIconDesc from '../../Assets/sortIconAsc.png';

import classes from '../Style/Table.module.css';

const ProgramTable = ({
  handlePageState,
  handleSortAction,
  handleSearchState,
  handleEditState,
  handleDeleteState,
  handleResetFilters,
  programs,
  paginationInfo,
  sortState,
  searchState
}) => {
  return (
    <div className="table__section table-responsive">
      <table className="table table-striped">
        <caption>List of programs</caption>
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
                  name="content"
                  className={classes.sortBlock}
                  onClick={handleSortAction}
                >
                  <img
                    src={
                      sortState.sort === 'content' && sortState.descending
                        ? sortIconDesc
                        : sortState.sort === 'content' && !sortState.descending
                        ? sortIconAsc
                        : sortIconDesc
                    }
                    alt="sorticon"
                  />{' '}
                </div>{' '}
                &nbsp; <span>Content:</span>
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
                name="content"
                className="form-control"
                type="text"
                placeholder="Content:"
                value={searchState.content}
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
          {programs &&
            programs.map((s, index) => {
              return (
                <tr key={s.id}>
                  <th scope="row">
                    <Button style={{ minWidth: 40 }} variant="success" disabled>
                      <span style={{ fontSize: 16 }}>
                        {index +
                          1 +
                          (paginationInfo.currentPage > 1
                            ? paginationInfo.currentPage *
                                paginationInfo.pageSize -
                              paginationInfo.pageSize
                            : 0)}
                      </span>
                    </Button>
                  </th>
                  <td>{s.name}</td>
                  <td>{s.content}</td>
                  <td>
                    <Link
                      to={
                        '/program/details?id=' +
                        s.id +
                        '&name=' +
                        s.name +
                        '&content=' +
                        s.content
                      }
                    >
                      <Button
                        className={classes.action__button}
                        variant="outline-success"
                      >
                        <input id={s.id} type="hidden" />
                        Details
                      </Button>
                    </Link>
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
                  </td>
                </tr>
              );
            })}
        </tbody>
      </table>
    </div>
  );
};

export default ProgramTable;
