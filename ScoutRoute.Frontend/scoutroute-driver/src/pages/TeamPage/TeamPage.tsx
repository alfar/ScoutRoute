import { useState } from 'react';
import { useAppDispatch, useAppSelector } from '../../store/hooks';
import { useTranslation } from 'react-i18next';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAdd, faTrash } from '@fortawesome/free-solid-svg-icons';
import { addMember, mutateTeamName, mutateTeamLead, mutateTeamPhone, removeMember, mutateTeamTrailerType } from '../../store/teamSlice';
import { QRCodeSVG } from 'qrcode.react';

export default function TeamPage() {
    const { t } = useTranslation();
    const dispatch = useAppDispatch();
    const team = useAppSelector(s => s.team);

    const [teamName, setTeamName] = useState(team.name);
    const [teamLead, setTeamLead] = useState(team.teamLead);
    const [phone, setPhone] = useState(team.phone);
    const [addMemberName, setAddMemberName] = useState("");

    const updateTeamNameAction = () => {
        dispatch(mutateTeamName(teamName || ""));
    };

    const updateTeamLeadAction = () => {
        dispatch(mutateTeamLead(teamLead || ""));
    }

    const updatePhoneAction = () => {
        dispatch(mutateTeamPhone(phone || ""));
    }

    const updateTrailerTypeAction = (e: React.ChangeEvent<HTMLSelectElement>) => {
        const trailerType = parseInt(e.target.value, 10);
        dispatch(mutateTeamTrailerType(trailerType));
    };

    const removeMemberAction = (index: number) => {
        dispatch(removeMember(index));
    }

    const addMemberAction = () => {
        if (addMemberName.trim().length > 0) {
            dispatch(addMember(addMemberName));
            setAddMemberName("");
        }
    }

    return (
        <>
            <div className="w-full p-2 border-2 border-t-0 border-gray-200 flex flex-col text-lg">
                <label className="block">{t("teamNameLabel")}</label>
                <input className="border-2 border-gray-200 w-full p-2 rounded-lg text-xl mb-3" type="text" value={teamName || ""} onChange={(e) => setTeamName(e.target.value)} onBlur={updateTeamNameAction} />

                <label className="block">{t("teamLeadLabel")}</label>
                <input className="border-2 border-gray-200 w-full p-2 rounded-lg text-xl mb-3" type="text" value={teamLead || ""} onChange={(e) => setTeamLead(e.target.value)} onBlur={updateTeamLeadAction} />

                <label className="block">{t("phoneLabel")}</label>
                <input className="border-2 border-gray-200 w-full p-2 rounded-lg text-xl mb-3" type="text" value={phone || ""} onChange={(e) => setPhone(e.target.value)} onBlur={updatePhoneAction} />

                <label className="block">{t("trailerTypeLabel")}</label>
                <select className="border-2 border-gray-200 w-full p-2 rounded-lg text-xl mb-3" value={team.trailerType || 0} onChange={updateTrailerTypeAction}>
                    <option value="1">{t("trailerType1")}</option>
                    <option value="2">{t("trailerType2")}</option>
                    <option value="3">{t("trailerType3")}</option>
                </select>

                <label className="block">{t("teamMembersLabel")}</label>
                <ul className="w-full flex flex-col gap-1">
                    {team.members?.map((m, i) => (
                        <li className="border-2 border-gray-200 rounded-lg flex justify-between items-center content-center" key={i}>
                            <div className="p-2">{m}</div>
                            <button className="bg-red-500 rounded-lg p-2 text-white text-xs mr-1" onClick={() => removeMemberAction(i)}><FontAwesomeIcon icon={faTrash} /></button>
                        </li>
                    ))}
                    <li>
                        <form action="" onSubmit={(e) => { e.preventDefault(); addMemberAction() }} className="border-2 border-gray-200 rounded-lg flex justify-between items-center">
                            <input className="border-b-1 border-gray-200 w-full p-2 focus:outline-0" type="text" value={addMemberName} onChange={(e) => setAddMemberName(e.target.value)} />
                            <button disabled={addMemberName.trim().length === 0} className="bg-blue-600 disabled:bg-gray-200 rounded-lg p-2 text-white text-xs mr-1" onClick={addMemberAction}><FontAwesomeIcon icon={faAdd} /></button>
                        </form>
                    </li>
                </ul>

                <QRCodeSVG className="mt-4 w-full" value={"https://whee.dk/scoutroute/team/switch/" + team.id} />
            </div>
        </>
    )
}