import React, { useCallback, useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';

import selectionService from '../../Services/selectionService';
import programService from '../../Services/programService';

import SelectionTable from './SelectionTable';
import SelectionActionForms from './SelectionActionForms';

import classes from '../Style/MainComponent.module.css';
import config from '../../Data/config';

const MainSelectionComponent = () => {
  const [availablePrograms, setAvailablePrograms] = useState([]);

  const INITIAL_SELECTION_FORM_DATA = {
    name: '',
    dateStart: '',
    dateEnd: '',
    status: '',
    japProgramId: ''
  };
  const [selectionFormData, setSelectionFormData] = useState(INITIAL_SELECTION_FORM_DATA);
  const [selections, setSelections] = useState([]);

  const INITIAL_SEARCH_STATE = {
    name: '',
    dateStart: '',
    dateEnd: '',
    status: '',
    japProgramName: ''
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

  const handleFetchAvailablePrograms = useCallback(() => {
    programService
      .fetchAllPrograms()
      .then(response => {
        setAvailablePrograms(response.data.data);
      })
      .catch(err => console.log(err));
  }, []);

  useEffect(() => {
    if ((actionState.action === 'add' || actionState.action === 'edit') && actionState.show)
      handleFetchAvailablePrograms();
  }, [actionState, handleFetchAvailablePrograms]);

  const fetchAllSelections = useCallback(params => {
    selectionService.fetchSelectionsParams(params).then(response => {
      setSelections(response.data.data.results);
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
    fetchAllSelections(params);
  }, [pageState, sortState, searchState, fetchAllSelections]);

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
    const value = ev.target.value;
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
      setSelectionFormData(() => {
        return INITIAL_SELECTION_FORM_DATA;
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
      const selection = selections.find(s => s.id === id);
      setSelectionFormData(() => {
        return {
          id: selection.id,
          name: selection.name,
          dateStart: selection.dateStart,
          dateEnd: selection.dateEnd,
          status: selection.status,
          japProgramId: selection.japProgram.id
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
      const selection = selections.find(s => s.id === id);
      setSelectionFormData(() => {
        return {
          id: selection.id
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

  const handleAddSelection = ev => {
    ev.preventDefault();
    selectionService
      .addSelection(selectionFormData)
      .then(response => {
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
        fetchAllSelections(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleEditSelection = ev => {
    ev.preventDefault();
    selectionService
      .modifySelection(selectionFormData)
      .then(response => {
        setActionState(() => {
          return {
            action: null,
            show: false
          };
        });
        setSelections(prevState => {
          let selection = prevState.findIndex(s => s.id === selectionFormData.id);
          prevState[selection] = response.data.data;
          return prevState;
        });
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleDeleteSelection = ev => {
    ev.preventDefault();
    selectionService
      .deleteSelection(selectionFormData)
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
        fetchAllSelections(params);
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleSelectionFormInput = ev => {
    const inputName = ev.target.name;
    const value = ev.target.value;
    setSelectionFormData(prevState => {
      let selection = {
        ...prevState
      };
      if (inputName === 'name') {
        selection.name = value;
      } else if (inputName === 'dateStart') {
        selection.dateStart = value;
      } else if (inputName === 'dateEnd') {
        selection.dateEnd = value;
      } else if (inputName === 'status') {
        selection.status = value;
      } else if (inputName === 'program') {
        selection.japProgramId = value;
      }
      return selection;
    });
  };

  const handleResetFilters = () => {
    setSearchState(INITIAL_SEARCH_STATE);
  };

  return (
    <div className={classes.table__container}>
      <div className={classes.student_table_actions}>
        <Button style={{ marginLeft: 20, minWidth: 170 }} variant="primary" onClick={handleAddState}>
          Add new selection
        </Button>
      </div>
      <SelectionActionForms
        formModel="selection"
        handleSelectionFormInput={handleSelectionFormInput}
        handleAddSelection={handleAddSelection}
        handleEditSelection={handleEditSelection}
        handleDeleteSelection={handleDeleteSelection}
        handleAddState={handleAddState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        actionState={actionState}
        selectionFormData={selectionFormData}
        availablePrograms={availablePrograms}
      />
      <SelectionTable
        handlePageState={handlePageState}
        handleSortAction={handleSortAction}
        handleSearchState={handleSearchState}
        handleEditState={handleEditState}
        handleDeleteState={handleDeleteState}
        handleResetFilters={handleResetFilters}
        selections={selections}
        paginationInfo={paginationInfo}
        sortState={sortState}
        searchState={searchState}
      />
    </div>
  );
};

export default MainSelectionComponent;
