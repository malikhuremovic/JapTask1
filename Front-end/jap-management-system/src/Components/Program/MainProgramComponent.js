import React, { useCallback, useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';

import ProgramActionForms from './ProgramActionForms';
import ProgramTable from './ProgramTable';

import programService from '../../Services/programService';

import classes from '../Style/MainComponent.module.css';
import config from '../../Data/config';

const MainProgramComponent = () => {
  const INITIAL_PROGRAM_FORM_DATA = {
    name: '',
    content: ''
  };

  const [programFormData, setProgramFormData] = useState(INITIAL_PROGRAM_FORM_DATA);

  const [programs, setPrograms] = useState([]);

  const INITIAL_SEARCH_STATE = {
    name: '',
    content: ''
  };
  const [searchState, setSearchState] = useState(INITIAL_SEARCH_STATE);
  const INITIAL_ACTION_STATE = {
    action: null,
    show: false
  };
  const [actionState, setActionState] = useState(INITIAL_ACTION_STATE);
  const INITIAL_SORT_STATE = {
    sort: 'name',
    descending: true
  };
  const [sortState, setSortState] = useState(INITIAL_SORT_STATE);
  const INITIAL_PAGE_STATE = {
    page: 1,
    pageSize: config.PAGE_SIZE
  };
  const [pageState, setPageState] = useState(INITIAL_PAGE_STATE);
  const INITIAL_PAGINATION_INFO_STATE = {
    currentPage: 0,
    pageCount: 0,
    pageSize: 0,
    recordCount: 0
  };
  const [paginationInfo, setPaginationInfoState] = useState(INITIAL_PAGINATION_INFO_STATE);

  const fetchAllPrograms = useCallback(params => {
    programService.fetchAllProgramsWithParams(params).then(response => {
      setPrograms(response.data.data.results);
      setPaginationInfoState(() => {
        const UPDATED_PAGINATION_INFO_STATE = {
          currentPage: response.data.data.currentPage,
          pageCount: response.data.data.pageCount,
          pageSize: response.data.data.pageSize,
          recordCount: response.data.data.recordCount
        };
        return UPDATED_PAGINATION_INFO_STATE;
      });
    });
  }, []);

  useEffect(() => {
    let params = {
      ...sortState,
      ...pageState,
      ...searchState
    };
    fetchAllPrograms(params);
  }, [pageState, sortState, searchState, fetchAllPrograms]);

  const handleSortAction = ev => {
    const sort = ev.target.parentNode.getAttribute('name');
    setSortState(previousSort => {
      const UPDATED_SORT = {
        sort,
        descending: sort === previousSort.sort ? !previousSort.descending : false
      };
      return UPDATED_SORT;
    });
  };

  const handlePageState = ev => {
    const action = ev.target.name;
    setPageState(prevState => {
      const UPDATED_PAGE_STATE = {
        page: action === 'prev' ? prevState.page-- : prevState.page++,
        pageSize: +prevState.pageSize
      };
      return UPDATED_PAGE_STATE;
    });
  };

  const handleSearchState = ev => {
    const field = ev.target.name;
    let value = ev.target.value;
    setSearchState(prevState => {
      let UPDATED_SEARCH_STATE = {
        ...prevState
      };
      UPDATED_SEARCH_STATE[field] = value;
      return UPDATED_SEARCH_STATE;
    });
  };

  const handleAddState = () => {
    if (!actionState.show) {
      setProgramFormData(() => {
        return INITIAL_PROGRAM_FORM_DATA;
      });
    }

    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'add',
        show: prevState.action === 'add' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleEditState = ev => {
    if (!actionState.show) {
      const id = +ev.target.childNodes[0].id;
      const program = programs.find(s => s.id === id);
      setProgramFormData(() => {
        return {
          id: program.id,
          name: program.name,
          content: program.content
        };
      });
    }

    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'edit',
        show: prevState.action === 'edit' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleDeleteState = ev => {
    if (!actionState.show) {
      const id = +ev.target.childNodes[0].id;
      const program = programs.find(s => s.id === id);
      setProgramFormData(() => {
        return {
          id: program.id
        };
      });
    }

    setActionState(prevState => {
      const UPDATED_ACTION_STATE = {
        action: 'delete',
        show: prevState.action === 'delete' ? !prevState.show : true
      };
      return UPDATED_ACTION_STATE;
    });
  };

  const handleAddProgram = (ev, items) => {
    ev.preventDefault();
    let ids = items.map(i => i.id);
    let formData = { ...programFormData };
    formData.lectures = ids;
    console.log(formData);
    programService
      .addProgram(formData)
      .then(() => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        let params = {
          ...sortState,
          ...pageState,
          ...searchState
        };
        fetchAllPrograms(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleEditProgram = ev => {
    ev.preventDefault();
    programService
      .editProgram(programFormData)
      .then(response => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        setPrograms(prevState => {
          let program = prevState.findIndex(s => s.id === programFormData.id);
          prevState[program] = response.data.data;
          return prevState;
        });
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleDeleteProgram = ev => {
    ev.preventDefault();
    programService
      .deleteProgram(programFormData)
      .then(() => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        let params = {
          ...sortState,
          ...pageState,
          ...searchState
        };
        fetchAllPrograms(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleProgramFormInput = ev => {
    const inputName = ev?.target?.name;
    const value = ev?.target?.value;
    setProgramFormData(prevState => {
      let program = {
        ...prevState
      };
      if (inputName === 'name') {
        program.name = value;
      } else if (inputName === 'content') {
        program.content = value;
      }
      return program;
    });
  };

  const handleResetFilters = () => {
    setSearchState(INITIAL_SEARCH_STATE);
  };

  return (
    <div className={classes.table__container}>
      <div className={classes.student_table_actions}>
        <Button style={{ marginLeft: 20, minWidth: 170 }} variant="primary" onClick={handleAddState}>
          Add new program
        </Button>
      </div>
      <ProgramActionForms
        formModel="program"
        handleProgramFormInput={handleProgramFormInput}
        handleAddProgram={handleAddProgram}
        handleEditProgram={handleEditProgram}
        handleDeleteProgram={handleDeleteProgram}
        handleAddState={handleAddState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        actionState={actionState}
        programFormData={programFormData}
      />
      <ProgramTable
        handlePageState={handlePageState}
        handleSortAction={handleSortAction}
        handleSearchState={handleSearchState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        handleResetFilters={handleResetFilters}
        programs={programs}
        paginationInfo={paginationInfo}
        sortState={sortState}
        searchState={searchState}
      />
    </div>
  );
};

export default MainProgramComponent;
